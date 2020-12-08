namespace GoLogs.Services.Customer.Api.Models
{
    public class CompanyInputDto
    {
        /// <summary>
        ///     The name of the company.
        /// </summary>
        /// <example>Johnny's Freight Forwarder</example>
        public string CompanyName { get; set; }

        /// <summary>
        ///     Nomor pokok wajib pajak. Unformatted.
        /// </summary>
        /// <example>000000000000000</example>
        public string Npwp { get; set; }

        /// <summary>
        ///     Nomor induk berusaha.
        /// </summary>
        /// <example>0000000000000</example>
        public string Nib { get; set; }

        /// <summary>
        ///     Nomor identitas kepabeanan.
        /// </summary>
        /// <example>00000000</example>
        public string Nik { get; set; }

        /// <summary>
        ///     Nomor pokok pengusaha pengurusan jasa kepabeanan.
        /// </summary>
        public string Npppjk { get; set; }

        public int CompanyTypeId { get; set; }

        public int? BrokerCompanyId { get; set; }
    }
}