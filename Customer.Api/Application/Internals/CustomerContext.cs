using GoLogs.Services.Customer.Api.Models;
using Microsoft.Extensions.Options;
using Nirbito.Framework.PostgresClient;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace GoLogs.Services.Customer.Api.Application.Internals
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomerContext : PgContext
    {
        public PgTable<CompanyType> CompanyTypes { get; set; }
        public PgTable<Company> Companies { get; set; }
        public PgTable<CompanyRole> CompanyRoles { get; set; }
        public PgTable<Person> Persons { get; set; }
        public PgTable<Tenant> Tenants { get; set; }

        public CustomerContext(IOptions<PgContextOptions> options) : base(options)
        {
        }
    }
}
