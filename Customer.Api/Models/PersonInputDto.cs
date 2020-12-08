namespace GoLogs.Services.Customer.Api.Models
{
    public class PersonInputDto
    {
        /// <summary>
        ///     Person's e-mail address.
        /// </summary>
        /// <example>johnny.bravo@inter.net</example>
        public string Email { get; set; }
        
        /// <summary>
        ///     Person's first name.
        /// </summary>
        /// <example>Johnny</example>
        public string Firstname { get; set; }

        /// <summary>
        ///     Person's last name.
        /// </summary>
        /// <example>Bravo</example>
        public string Lastname { get; set; }

        /// <summary>
        ///     Nomor pokok wajib pajak. Unformatted.
        /// </summary>
        /// <example>000000000000000</example>
        public string Npwp { get; set; }
        
        public int? CompanyId { get; set; }
        public int? CompanyRoleId { get; set; }
    }
}
