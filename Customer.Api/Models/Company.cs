// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

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
