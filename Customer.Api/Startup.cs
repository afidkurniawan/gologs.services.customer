// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using FluentValidation.AspNetCore;
using GoLogs.Framework.Core.Options;
using GoLogs.Framework.Mvc;
using GoLogs.Services.Customer.Api.Application.Internals;
using GoLogs.Services.Customer.Api.BusinessLogic;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nirbito.Framework.PostgresClient;
using Nirbito.Framework.PostgresClient.DependencyInjectionExtensions;
using Nirbito.Framework.PostgresClient.ManagedColumns;
using Swashbuckle.AspNetCore.Swagger;

namespace GoLogs.Services.Customer.Api
{
    public class Startup
    {
        private ServiceOptions _rabbitMqOptions;

        public Startup(IConfiguration configuration)
        {
            AssemblyName = Assembly.GetEntryAssembly()?.GetName();
            Configuration = configuration;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public IConfiguration Configuration { get; }

        private AssemblyName AssemblyName { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddControllers()
                .AddFluentValidation()
                .AddNewtonsoftJson();

            services
                .AddSingleton<ScopedHttpContext>()
                .AddOptions<PgContextOptions>()
                .Configure<ScopedHttpContext>((options, context) => options.DefaultColumns =
                    new Collection<IDefaultColumn>
                    {
                        new DefaultColumn<DateTime?>(
                            "created", (insert, _) =>
                                insert ? (DateTime?)DateTime.Now : null),
                        new DefaultColumn<string>(
                            "creator", (insert, _) =>
                                insert ? context?.Accessor.HttpContext.User.Identity.Name ?? "ANONYMOUS" : null),
                        new DefaultColumn<DateTime?>(
                            "modified", (_, update) =>
                                update ? (DateTime?)DateTime.Now : null),
                        new DefaultColumn<string>(
                            "modifier", (_, update) =>
                                update ? context?.Accessor.HttpContext.User.Identity.Name ?? "ANONYMOUS" : null)
                    });

            services
                .AddPgContext<CustomerContext>(options => options
                    .UseConnectionString(Configuration.GetConnectionString("CustomerDb"))
                    .UseSoftDeleteColumn(new SoftDeleteColumn<int>(
                        "rowstatus", delete => delete ? 1 : 0))
                    .UseNamingConvention(NamingConvention.SnakeCase));

            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq(ConfigureRabbitMq);
            });

            services
                .AddScoped<IProblemCollector, ProblemCollector>()
                .AddScoped<ICustomerLogic, CustomerLogic>()
                .AddFluentValidators()
                .AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = AssemblyName.Name,
                        Version = "v1"
                    });

                var filePath = System.IO.Path.Combine(
                    AppContext.BaseDirectory,
                    String.Concat(AssemblyName.Name, ".xml"));
                c.IncludeXmlComments(filePath);
                c.AddFluentValidationRules();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        "swagger/v1/swagger.json",
                        String.Concat(AssemblyName.Name, " v1"));
                    c.RoutePrefix = String.Empty;
                });
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private X509Certificate CertificateSelectionCallback(
            object sender, string targetHost, X509CertificateCollection localCertificates,
            X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            var serverCertificate = localCertificates.OfType<X509Certificate2>()
                .FirstOrDefault(cert => cert.Thumbprint?.ToUpperInvariant() == _rabbitMqOptions.SslThumbprint.ToUpperInvariant());

            return serverCertificate ?? throw new System.Security.SecurityException("Wrong certificate");
        }

        private void ConfigureRabbitMq(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator rabbitMqCfg)
        {
            _rabbitMqOptions = Configuration.GetSection(ServiceDependenciesOptions.ServiceDependencies)
                .Get<ServiceOptions[]>()
                .First(svc => svc.Name.Equals("RabbitMQ", StringComparison.Ordinal));

            X509Certificate2 x509Certificate2 = null;

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            try
            {
                var certificatesInStore = store.Certificates;

                x509Certificate2 = certificatesInStore.OfType<X509Certificate2>()
                    .FirstOrDefault(cert => cert.Thumbprint?.ToUpperInvariant() == _rabbitMqOptions.SslThumbprint?.ToUpperInvariant());
            }
            finally
            {
                store.Close();
            }

            rabbitMqCfg.Host(_rabbitMqOptions.Host, _rabbitMqOptions.VirtualHost, h =>
            {
                h.Username(_rabbitMqOptions.Username);
                h.Password(_rabbitMqOptions.Password);

                if (_rabbitMqOptions.UseSsl)
                {
                    h.UseSsl(ssl =>
                    {
                        ssl.ServerName = Dns.GetHostName();
                        ssl.AllowPolicyErrors(SslPolicyErrors.RemoteCertificateNameMismatch);
                        ssl.Certificate = x509Certificate2;
                        ssl.CertificateSelectionCallback = CertificateSelectionCallback;
                    });
                }
            });

            rabbitMqCfg.ConfigureEndpoints(context);
        }
    }
}
