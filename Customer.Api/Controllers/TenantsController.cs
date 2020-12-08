using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Events;
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
    public class TenantsController : Controller
    {
        public TenantsController(ICustomerLogic customerLogic, IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint) :
            base(customerLogic, problemCollector, mapper, publishEndpoint)
        {
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Tenant>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var tenant = await CustomerLogic.GetTenantByIdAsync(id);

            if (tenant != null)
            {
                return Ok(tenant);
            }

            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Tenant>> GetAsync(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            var tenant = await CustomerLogic.GetTenantByNameAsync(name);

            if (tenant != null)
            {
                return Ok(tenant);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody]TenantInputDto tenantInput)
        {
            var tenant = Mapper.Map<Tenant>(tenantInput);
            await CustomerLogic.CreateTenantAsync(tenant);

            var errorResult = CheckProblems();
            if (errorResult != null)
            {
                return errorResult;
            }

            await PublishEndpoint.Publish<ITenantCreatedEvent>(new {Tenant = tenant});
            
            return CreatedAtAction(
                Url.Action(nameof(GetAsync)), new { id = tenant.Id }, tenant);
        }
    }
}
