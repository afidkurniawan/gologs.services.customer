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
    public class CompanyTypesController : Controller
    {
        public CompanyTypesController(ICustomerLogic customerLogic, IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint) : base(customerLogic, problemCollector, mapper, publishEndpoint)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await CustomerLogic.GetAllCompanyTypesAsync());
        }
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CompanyType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyType>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var companyType = await CustomerLogic.GetCompanyTypeByIdAsync(id);

            if (companyType != null)
            {
                return Ok(companyType);
            }

            return NotFound();
        }
        
        [HttpGet]
        [Route("{id:int}/Companies")]
        [ProducesResponseType(typeof(IEnumerable<CompanyType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompaniesAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return Ok(await CustomerLogic.GetCompaniesByCompanyTypeIdAsync(id));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody]CompanyTypeInputDto companyTypeInput)
        {
            var companyType = Mapper.Map<CompanyType>(companyTypeInput);
            await CustomerLogic.CreateCompanyTypeAsync(companyType);

            var errorResult = CheckProblems();
            return errorResult ?? CreatedAtAction(
                Url.Action(nameof(GetAsync)), new { id = companyType.Id }, companyType);
        }

        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(IEnumerable<CompanyType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody]JsonPatchDocument<CompanyTypeInputDto> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            var patchTester = new CompanyTypeInputDto();
            patchDoc.ApplyTo(patchTester, ModelState);
            TryValidateModel(patchTester);
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var companyType = new CompanyType {Id = id};
            patchDoc.ApplyTo(companyType);

            if (await CustomerLogic.UpdateCompanyTypeAsync(companyType))
            {
                return Ok();
            }
            
            var errorResult = CheckProblems();
            return (ActionResult) errorResult ?? NoContent();
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

            if (await CustomerLogic.DeleteCompanyTypeAsync(id))
            {
                return Ok();
            }
            
            var errorResult = CheckProblems();
            return (ActionResult) errorResult ?? NotFound();
        }
    }
}
