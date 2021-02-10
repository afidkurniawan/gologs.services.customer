// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

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

        protected ICustomerLogic CustomerLogic { get; }

        protected IMapper Mapper { get; }

        protected IPublishEndpoint PublishEndpoint { get; }

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
            if (!_problemCollector.HasProblems)
            {
                return null;
            }

            var problem = _problemCollector.GetProblems();
            return StatusCode(problem.Status.GetValueOrDefault(), problem);
        }
    }
}
