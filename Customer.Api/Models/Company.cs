using System.ComponentModel.DataAnnotations;

namespace GoLogs.Services.Customer.Api.Models
{
    public class Company : CompanyInputDto, IEntity
    {
        public CompanyType CompanyType { get; set; }
        public Company BrokerCompany { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
