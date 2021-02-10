// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using FluentValidation;
using GoLogs.Services.Customer.Api.Application.Validators;
using GoLogs.Services.Customer.Api.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GoLogs.Services.Customer.Api
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds all required <see cref="IValidator"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            return services
                .AddTransient<IValidator<CompanyTypeInputDto>, CompanyTypeValidator>()
                .AddTransient<IValidator<CompanyInputDto>, CompanyValidator>()
                .AddTransient<IValidator<CompanyType>, CompanyTypeValidator>()
                .AddTransient<IValidator<Company>, CompanyValidator>()
                .AddTransient<IValidator<CompanyRoleInputDto>, CompanyRoleValidator>()
                .AddTransient<IValidator<PersonInputDto>, PersonValidator>()
                .AddTransient<IValidator<TenantInputDto>, TenantValidator>();
        }
    }
}
