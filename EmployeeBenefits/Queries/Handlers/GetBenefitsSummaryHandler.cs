using EmployeeBenefits.Business;
using EmployeeBenefits.Queries.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Queries.Handlers
{
    public class GetBenefitsSummaryHandler : IRequestHandler<GetBenefitsSummary, BenefitsSummary>
    {
        private readonly IMediator _mediator;
        private readonly ISummarizeBenefits _summarizeBenefits;

        public GetBenefitsSummaryHandler(IMediator mediator, ISummarizeBenefits summarizeBenefits)
        {
            _mediator = mediator;
            _summarizeBenefits = summarizeBenefits;
        }

        public BenefitsSummary Handle(GetBenefitsSummary message)
        {
            var msg = new GetBenefitsDataMessage
            {
                EmployeeId = message.EmployeeId
            };

            var data = _mediator.Send(msg).Result;

            var summary = _summarizeBenefits.Run(data);

            return new BenefitsSummary();
        }
    }
}
