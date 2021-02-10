// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace GoLogs.Services.Customer.Api.Models
{
    public class CompanyRole : CompanyRoleInputDto, IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
