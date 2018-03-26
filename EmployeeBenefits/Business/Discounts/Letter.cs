using System;

namespace EmployeeBenefits.Business.Discounts
{
    public class Letter
    {
        public Func<string, string, decimal, decimal> GetDiscount = SumDiscount;

        public static decimal SumDiscount(string name, string promoTrigger, decimal discountAmt)
        {
            var value = 0M;
            
            if (name.StartsWith(promoTrigger))
            {
                value = discountAmt;
            }

            return value;
        }
    }
}
