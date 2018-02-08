using EmployeeBenefits.Business;
using MediatR;

namespace EmployeeBenefits.Queries.Messages
{
    public class GetBenefitsSummary : IRequest<BenefitsSummary>
    {
        public int EmployeeId { get; set; }
    }
}
