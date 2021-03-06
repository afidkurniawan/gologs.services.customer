namespace GoLogs.Services.Customer.Api.Models
{
    public class TenantInputDto
    {
        public int CompanyId { get; set; }

        /// <summary>
        ///     The name of the tenant to be used for db name. The name must be a valid SQL identifier.
        /// </summary>
        public string TenantName { get; set; }
    }
}
