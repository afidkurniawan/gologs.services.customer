using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Services.Customer.Api.BusinessLogic;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.Customer.Api.Controllers
{
    public class Controller : ControllerBase
    {
        private readonly IProblemCollector _problemCollector;

        protected readonly ICustomerLogic CustomerLogic;
        protected readonly IMapper Mapper;
        protected readonly IPublishEndpoint PublishEndpoint;
        
        public Controller(ICustomerLogic customerLogic, IProblemCollector problemCollector,
            IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _problemCollector = problemCollector;
            CustomerLogic = customerLogic;
            Mapper = mapper;
            PublishEndpoint = publishEndpoint;
        }

        protected ObjectResult CheckProblems()
        {
            if (!_problemCollector.HasProblems) return null;
            
            var problem = _problemCollector.GetProblems();
            return StatusCode(problem.Status.GetValueOrDefault(), problem);
        }
    }
}
