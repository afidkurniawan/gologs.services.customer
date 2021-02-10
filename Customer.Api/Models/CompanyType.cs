// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace GoLogs.Services.Customer.Api.Models
{
    public class CompanyType : CompanyTypeInputDto, IEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     If <c>true</c>, this <see cref="CompanyType"/> is a tenant. Tenant companies have their own
        ///     databases for every service.
        /// </summary>
        public bool IsTenantType { get; set; }
    }
}
