namespace EmployeeBenefits.Business
{
    public class GetEmployeeDiscountAmount 
    {
        private readonly BenefitsSummary _context;

        public GetEmployeeDiscountAmount(BenefitsSummary context)
        {
            _context = context;
        }

        public void Run()
        {
            var value = 0M;

            if (!_context.EmployeeLastName.StartsWith(_context.DiscountTrigger))
                return;

            value = (_context.EmployeeCostOfBenefits * _context.DiscountAmount);

            _context.EmployeeDiscountAmount = value;
        }
    }
}
