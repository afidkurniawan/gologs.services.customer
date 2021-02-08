// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

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
