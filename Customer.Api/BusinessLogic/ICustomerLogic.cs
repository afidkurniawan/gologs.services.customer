// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api.BusinessLogic
{
    public interface ICustomerLogic
    {
        Task<CompanyType[]> GetAllCompanyTypesAsync();

        Task<CompanyType> GetCompanyTypeByIdAsync(int id);

        Task CreateCompanyTypeAsync(CompanyType newCompanyType);

        Task<bool> UpdateCompanyTypeAsync(CompanyType update);

        Task<bool> DeleteCompanyTypeAsync(int id);

        Task<Company[]> GetAllCompaniesAsync();

        Task<Company> GetCompanyByIdAsync(int id);

        Task<IList<Company>> GetCompaniesByCompanyTypeIdAsync(int id);

        Task CreateCompanyAsync(Company newCompany);

        Task CreateTenantCompanyAsync(Company newCompany, Tenant newTenant);

        Task<bool> UpdateCompanyAsync(Company update);

        Task<bool> DeleteCompanyAsync(int id);

        Task<CompanyRole> GetCompanyRoleByIdAsync(int id);

        Task CreateCompanyRoleAsync(CompanyRole newCompanyRole);

        Task CreateTenantAsync(Tenant newTenant);

        Task<Tenant> GetTenantByIdAsync(int id);

        Task<Tenant> GetTenantByNameAsync(string name);

        Task<object> GetPersonByIdAsync(int id);

        Task CreatePersonAsync(Person newPerson);
    }
}
