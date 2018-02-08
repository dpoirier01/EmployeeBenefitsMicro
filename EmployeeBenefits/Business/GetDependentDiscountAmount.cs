using System.Linq;

namespace EmployeeBenefits.Business
{
    public class GetDependentDiscountAmount
    {
        private readonly BenefitsSummary _context;

        public GetDependentDiscountAmount(BenefitsSummary context)
        {
            _context = context;
        }

        public void Run()
        {
            var value = 0M;

            foreach (var d in _context.DependentsList.Select(x => x.LastName))
            {
                if (d.StartsWith(_context.DiscountTrigger))
                {
                    value += (_context.DependentCostOfBenefits * _context.DiscountAmount);
                }
            }

            _context.DependentDiscountAmount = value;
        }
    }
}
