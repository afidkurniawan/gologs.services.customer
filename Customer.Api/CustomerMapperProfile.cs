// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using AutoMapper;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api
{
    // ReSharper disable once UnusedType.Global
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateMap<CompanyTypeInputDto, CompanyType>();
            CreateMap<CompanyInputDto, Company>();
            CreateMap<CompanyRoleInputDto, CompanyRole>();
            CreateMap<PersonInputDto, Person>();
            CreateMap<TenantInputDto, Tenant>();
        }
    }
}
