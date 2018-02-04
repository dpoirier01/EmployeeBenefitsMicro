﻿using EmployeeBenefits.Framework.Tasks;

namespace EmployeeBenefits.Business
{
    public class GetTotalAfterDiscount : ITask
    {
        private readonly BenefitsSummary _context;

        public GetTotalAfterDiscount(BenefitsSummary context)
        {
            _context = context;
        }

        public void Run()
        {
            var value = 0M;

            value = _context.TotalCostBeforeDiscount - _context.TotalDiscountAmount;

            _context.TotalAfterDiscount = value;
        }
    }
}