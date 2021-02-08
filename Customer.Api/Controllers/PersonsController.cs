// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

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
    public class PersonsController : Controller
    {
        public PersonsController(ICustomerLogic customerLogic, IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint)
            : base(customerLogic, problemCollector, mapper, publishEndpoint)
        {
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var person = await CustomerLogic.GetPersonByIdAsync(id);

            if (person != null)
            {
                return Ok(person);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ProblemDetails>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody] PersonInputDto personInput)
        {
            var person = Mapper.Map<Person>(personInput);
            await CustomerLogic.CreatePersonAsync(person);

            var errorResult = CheckProblems();
            if (errorResult != null)
            {
                return errorResult;
            }

            await PublishEndpoint.Publish<IPersonCreatedEvent>(new { Person = person });

            return CreatedAtAction(
                Url.Action(nameof(GetAsync)), new { id = person.Id }, person);
        }
    }
}
