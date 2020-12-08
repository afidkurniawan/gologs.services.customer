using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.Customer.Api.Models
{
    public class Tenant : TenantInputDto, IEntity, ITenant
    {
        [Key]
        public int Id { get; set; }

        public Company Company { get; set; }
    }
}
