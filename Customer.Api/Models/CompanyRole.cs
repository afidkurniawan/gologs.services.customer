using System.ComponentModel.DataAnnotations;

namespace GoLogs.Services.Customer.Api.Models
{
    public class CompanyRole : CompanyRoleInputDto, IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}