using EmployeeBenefits.Data.Entities;
using EmployeeBenefits.Framework.Tasks;

namespace EmployeeBenefits.Business
{
    public class GetDependentCostBeforeDiscount : ITask
    {

        private readonly BenefitsSummary _context;

        public GetDependentCostBeforeDiscount(BenefitsSummary context)
        {
            _context = context;
        }

        public void Run()
        {
            var value = 0M;

            foreach (Dependent d in _context.DependentsList)
            {
                value += _context.DependentCostOfBenefits;
            }

            _context.DependentCostBeforeDiscount = value;
        }
    }
}
