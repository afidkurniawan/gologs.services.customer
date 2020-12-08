using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Services.Customer.Api.BusinessLogic;
using GoLogs.Services.Customer.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.Customer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : Controller
    {
        public RolesController(ICustomerLogic customerLogic, IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint) : base(customerLogic, problemCollector, mapper, publishEndpoint)
        {
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyRole>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var role = await CustomerLogic.GetCompanyRoleByIdAsync(id);

            if (role != null)
            {
                return Ok(role);
            }

            return NotFound();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody]CompanyRoleInputDto companyRoleInput)
        {
            var companyRole = Mapper.Map<CompanyRole>(companyRoleInput);
            await CustomerLogic.CreateCompanyRoleAsync(companyRole);

            var errorResult = CheckProblems();
            if (errorResult != null)
            {
                return errorResult;
            }

            return CreatedAtAction(
                Url.Action(nameof(GetAsync)), new { id = companyRole.Id }, companyRole);
        }
    }
}