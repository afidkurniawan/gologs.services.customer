using AutoMapper;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api
{
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
