using EmployeeBenefits.Framework.Tasks;

namespace EmployeeBenefits.Business
{
    public class GetTotalDiscountAmount : ITask
    {
        private readonly BenefitsSummary _context;

        public GetTotalDiscountAmount(BenefitsSummary context)
        {
            _context = context;
        }

        public void Run()
        {
            var value = 0M;

            value = (_context.EmployeeDiscountAmount + _context.DependentDiscountAmount);

            _context.TotalDiscountAmount = value;
        }
    }
}
