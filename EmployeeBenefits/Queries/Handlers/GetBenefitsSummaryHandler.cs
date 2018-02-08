using EmployeeBenefits.Business;
using EmployeeBenefits.Queries.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Queries.Handlers
{
    public class GetBenefitsSummaryHandler : IRequestHandler<GetBenefitsDataSummary, BenefitsSummary>
    {
        private readonly IMediator _mediator;

        public GetBenefitsSummaryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public BenefitsSummary Handle(GetBenefitsDataSummary message)
        {
            var msg = new GetBenefitsDataMessage
            {
                EmployeeId = message.EmployeeId
            };

            var results = _mediator.Send(msg);

            return new BenefitsSummary();
        }
    }
}
