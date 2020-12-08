using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.Customer.Api.Models
{
    public class Person : PersonInputDto, IEntity, IPerson
    {
        public Company Company { get; set; }
        public CompanyRole CompanyRole { get; set; }
        
        [Key]
        public int Id { get; set; }
    }
}