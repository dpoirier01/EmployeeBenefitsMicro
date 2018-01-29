using EmployeeBenefits.Queries.Results;
using MediatR;
using System.Collections.Generic;

namespace EmployeeBenefits.Queries.Messages
{
    public class GetBenefitsSummaryMessage : IRequest<List<GetBenefitsSummaryResults>>
    {
        public int EmployeeId { get; set; }
    }
}
