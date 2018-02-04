using EmployeeBenefits.Framework.Tasks;

namespace EmployeeBenefits.Business
{
    public class GetTotalBeforeDiscount : ITask
    {

        private readonly BenefitsSummary _context;

        public GetTotalBeforeDiscount(BenefitsSummary context)
        {
            _context = context;
        }

        public void Run()
        {
            var value = _context.EmployeeCostOfBenefits + _context.DependentCostBeforeDiscount;

            _context.TotalCostBeforeDiscount = value;
        }
    }
}
