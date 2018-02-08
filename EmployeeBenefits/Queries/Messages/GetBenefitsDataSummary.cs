using EmployeeBenefits.Business;
using MediatR;

namespace EmployeeBenefits.Queries.Messages
{
    public class GetBenefitsDataSummary : IRequest<BenefitsSummary>
    {
        public int EmployeeId { get; set; }
    }
}
