// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Services.Customer.Api.Application.Internals;
using GoLogs.Services.Customer.Api.Models;
using Nirbito.Framework.PostgresClient.Exceptions;
using SqlKata;

namespace GoLogs.Services.Customer.Api.BusinessLogic
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly CustomerContext _context;
        private readonly IProblemCollector _problemCollector;

        public CustomerLogic(CustomerContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        #region CompanyType

        public async Task<CompanyType[]> GetAllCompanyTypesAsync()
        {
            return await _context.CompanyTypes.ToArrayAsync();
        }

        #endregion

        #region Company

        public async Task<Company[]> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToArrayAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.GetAsync(id);
        }

        public async Task<IList<Company>> GetCompaniesByCompanyTypeIdAsync(int id)
        {
            return await _context.Companies.AllAsync(
                new Query().Where(nameof(Company.CompanyTypeId), id));
        }

        public async Task CreateCompanyAsync(Company newCompany)
        {
            try
            {
                await _context.Companies.InsertAsync(newCompany);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(newCompany);
            }
        }

        public async Task CreateTenantCompanyAsync(Company newCompany, Tenant newTenant)
        {
            newCompany.CompanyType ??= await GetCompanyTypeByIdAsync(newCompany.CompanyTypeId);
            if (!newCompany.CompanyType.IsTenantType)
            {
                _problemCollector.AddProblem(new CodedProblemDetails(
                    ProblemType.ERROR_DATA_INVALID,
                    $"{nameof(Company)}.{nameof(Company.CompanyType)}",
                    newCompany.CompanyType.TypeName));
                return;
            }

            if (await GetTenantByNameAsync(newTenant.TenantName) != default(Tenant))
            {
                AddDataAlreadyExistsProblem(new TenantInputDto
                {
                    TenantName = newTenant.TenantName
                });
                return;
            }

            await CreateCompanyAsync(newCompany);
            await CreateTenantAsync(newTenant);
        }

        public async Task<bool> UpdateCompanyAsync(Company update)
        {
            var updatedCount = 0;
            try
            {
                updatedCount = await _context.Companies.UpdateAsync(update);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(update);
            }

            return updatedCount > 0;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var deletedCount = await _context.Companies.DeleteAsync(id);
            return deletedCount > 0;
        }

        #endregion

        #region CompanyRole

        public async Task<CompanyRole> GetCompanyRoleByIdAsync(int id)
        {
            return await _context.CompanyRoles.GetAsync(id);
        }

        public async Task CreateCompanyRoleAsync(CompanyRole newCompanyRole)
        {
            try
            {
                await _context.CompanyRoles.InsertAsync(newCompanyRole);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(newCompanyRole);
            }
        }

        #endregion

        public async Task CreateTenantAsync(Tenant newTenant)
        {
            try
            {
                await _context.Tenants.InsertAsync(newTenant);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(newTenant);
            }
        }

        public async Task<Tenant> GetTenantByIdAsync(int id)
        {
            return await _context.Tenants.GetAsync(id);
        }

        public async Task<Tenant> GetTenantByNameAsync(string name)
        {
            return await _context.Tenants.FirstOrDefaultAsync(
                new Query().Where(nameof(Tenant.TenantName), name));
        }

        public async Task<object> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.GetAsync(id);
        }

        public async Task CreatePersonAsync(Person newPerson)
        {
            try
            {
                await _context.Persons.InsertAsync(newPerson);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(newPerson);
            }
        }

        public async Task<CompanyType> GetCompanyTypeByIdAsync(int id)
        {
            return await _context.CompanyTypes.GetAsync(id);
        }

        public async Task CreateCompanyTypeAsync(CompanyType newCompanyType)
        {
            try
            {
                await _context.CompanyTypes.InsertAsync(newCompanyType);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(newCompanyType);
            }
        }

        public async Task<bool> UpdateCompanyTypeAsync(CompanyType update)
        {
            var updatedCount = 0;
            try
            {
                updatedCount = await _context.CompanyTypes.UpdateAsync(update);
            }
            catch (DataAlreadyExistsException)
            {
                AddDataAlreadyExistsProblem(update);
            }

            return updatedCount > 0;
        }

        public async Task<bool> DeleteCompanyTypeAsync(int id)
        {
            var deletedCount = await _context.CompanyTypes.DeleteAsync(id);
            return deletedCount > 0;
        }

        #region ERROR_DATA_ALREADY_EXISTS

        private void AddDataAlreadyExistsProblem(CompanyTypeInputDto companyType)
        {
            _problemCollector.AddProblem(new CodedProblemDetails(
                ProblemType.ERROR_DATA_ALREADY_EXISTS,
                $"{nameof(CompanyType)}.{nameof(CompanyType.TypeName)}",
                companyType.TypeName));
        }

        private void AddDataAlreadyExistsProblem(CompanyInputDto company)
        {
            _problemCollector.AddProblem(new CodedProblemDetails(
                ProblemType.ERROR_DATA_ALREADY_EXISTS,
                $"{nameof(Company)}.{nameof(Company.Npwp)}",
                company.Npwp));
        }

        private void AddDataAlreadyExistsProblem(CompanyRoleInputDto companyRole)
        {
            _problemCollector.AddProblem(new CodedProblemDetails(
                ProblemType.ERROR_DATA_ALREADY_EXISTS,
                $"{nameof(CompanyRole)}.{nameof(CompanyRole.RoleName)}",
                companyRole.RoleName));
        }

        private void AddDataAlreadyExistsProblem(PersonInputDto person)
        {
            _problemCollector.AddProblem(new CodedProblemDetails(
                ProblemType.ERROR_DATA_ALREADY_EXISTS,
                $"{nameof(Person)}.{nameof(Person.Npwp)}",
                person.Npwp));
        }

        private void AddDataAlreadyExistsProblem(TenantInputDto tenant)
        {
            _problemCollector.AddProblem(new CodedProblemDetails(
                ProblemType.ERROR_DATA_ALREADY_EXISTS,
                $"{nameof(Tenant)}.{nameof(tenant.TenantName)}",
                tenant.TenantName));
        }

        #endregion
    }
}
