using EmployeeBenefits.Queries.Results;
using MediatR;
using System.Collections.Generic;

namespace EmployeeBenefits.Queries.Messages
{
    public class GetBenefitsDataMessage : IRequest<GetBenefitsDataResults>
    {
        public int EmployeeId { get; set; }
    }
}
