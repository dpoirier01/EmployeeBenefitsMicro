using EmployeeBenefits.Data.Entities;
using EmployeeBenefits.Framework.Tasks;
using EmployeeBenefits.Queries.Results;
using System.Linq;

namespace EmployeeBenefits.Business
{
    public class SummarizeBenefits : ISummarizeBenefits 
    {

        private readonly IProcess<BenefitsSummary> _process;

        public SummarizeBenefits(IProcess<BenefitsSummary> process)
        {
            _process = process;
        }

        public BenefitsSummary Run(GetBenefitsDataResults data)
        {
            var results = new BenefitsSummary()
            {
                EmployeeFirstName = data.Employee.FirstName,
                EmployeeLastName = data.Employee.LastName,
                EmployeeCostOfBenefits = data.Benefit.EmployeeCost,
                DiscountTrigger = data.Promotions.Select(x => x.PromotionTrigger).FirstOrDefault(),
                DiscountAmount = data.Promotions.Select(x => x.DiscountAmount).FirstOrDefault(),
                DependentsList = data.Dependent,
                DependentCostOfBenefits = data.Benefit.DependentCost,
                EmployeeFullName = data.Employee.FirstName + " " + data.Employee.LastName
            };

            _process.With(results)
                .Do<GetDependentCostBeforeDiscount>()
                .Do<GetTotalBeforeDiscount>()
                .Do<GetEmployeeDiscountAmount>()
                .Do<GetDependentDiscountAmount>()
                .Do<GetTotalDiscountAmount>()
                .Do<GetTotalAfterDiscount>();

            return results;
        }
    }
}
