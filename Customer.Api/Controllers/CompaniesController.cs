// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Services.Customer.Api.BusinessLogic;
using GoLogs.Services.Customer.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.Customer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : Controller
    {
        public CompaniesController(ICustomerLogic customerLogic, IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint)
            : base(customerLogic, problemCollector, mapper, publishEndpoint)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Company>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await CustomerLogic.GetAllCompaniesAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var company = await CustomerLogic.GetCompanyByIdAsync(id);

            if (company != null)
            {
                return Ok(company);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody] CompanyInputDto companyInput)
        {
            var company = Mapper.Map<Company>(companyInput);
            await CustomerLogic.CreateCompanyAsync(company);

            var errorResult = CheckProblems();
            return errorResult ?? CreatedAtAction(
                Url.Action(nameof(GetAsync)), new { id = company.Id }, company);
        }

        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<CompanyInputDto> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var patchTester = new CompanyInputDto();
            patchDoc.ApplyTo(patchTester, ModelState);
            TryValidateModel(patchTester);
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var company = new Company { Id = id };
            patchDoc.ApplyTo(company);

            if (await CustomerLogic.UpdateCompanyAsync(company))
            {
                return Ok();
            }

            var errorResult = CheckProblems();
            return (ActionResult)errorResult ?? NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await CustomerLogic.DeleteCompanyAsync(id))
            {
                return Ok();
            }

            var errorResult = CheckProblems();
            return (ActionResult)errorResult ?? NotFound();
        }
    }
}
