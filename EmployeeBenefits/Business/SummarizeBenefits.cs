using EmployeeBenefits.Data.Entities;
using EmployeeBenefits.Queries.Results;
using System.Linq;

namespace EmployeeBenefits.Business
{
    public class SummarizeBenefits : ISummarizeBenefits 
    {
        public BenefitsSummary Run(GetBenefitsDataResults data)
        {
            var results = new BenefitsSummary();
            
            // automapper?
            results.EmployeeFirstName = data.Employee.FirstName;
            results.EmployeeLastName = data.Employee.LastName;
            results.EmployeeCostOfBenefits = data.Benefit.EmployeeCost;
            results.DiscountTrigger = data.Promotions.Select(x => x.PromotionTrigger).FirstOrDefault();
            results.DiscountAmount = data.Promotions.Select(x => x.DiscountAmount).FirstOrDefault();
            results.DependentsList = data.Dependent;
            results.DependentCostOfBenefits = data.Benefit.DependentCost;

            results.EmployeeFullName = GetEmployeeFullName(data);
            results.DependentCostBeforeDiscount = GetDependentCostBeforeDiscount(data);
            results.TotalCostBeforeDiscount = GetTotalBeforeDiscount(results);
            results.EmployeeDiscountAmount = GetEmployeeDiscountAmount(results);
            results.DependentDiscountAmount = GetDependentDiscountAmount(results);
            results.TotalDiscountAmount = GetTotalDiscountAmount(results);
            results.TotalAfterDiscount = GetTotalAfterDiscount(results);

            return results;
        }

        public string GetEmployeeFullName(GetBenefitsDataResults data)
        {
            return data.Employee.FirstName + " " + data.Employee.LastName;
        }
        
        public decimal GetDependentCostBeforeDiscount(GetBenefitsDataResults data)
        {
            var value = 0M;

            foreach (Dependent d in data.Dependent)
            {
                value += data.Benefit.DependentCost;
            }

            return value;
        }

        public decimal GetTotalBeforeDiscount(BenefitsSummary summary)
        {
            var value = summary.EmployeeCostOfBenefits + summary.DependentCostBeforeDiscount;

            return value;
        }

        public decimal GetEmployeeDiscountAmount(BenefitsSummary data)
        {
            var value = 0M;

            if (!data.EmployeeLastName.StartsWith(data.DiscountTrigger))
                return value;

            value = (data.EmployeeCostOfBenefits * data.DiscountAmount);

            return value;
        }

        public decimal GetDependentDiscountAmount(BenefitsSummary data)
        {
            var value = 0M;

            foreach(var d in data.DependentsList.Select(x => x.LastName))
            {
                if (d.StartsWith(data.DiscountTrigger))
                {
                    value += (data.DependentCostOfBenefits * data.DiscountAmount);
                }
            }

            return value;
        }

        public decimal GetTotalDiscountAmount(BenefitsSummary data)
        {
            var value = 0M;

            value = (data.EmployeeDiscountAmount + data.DependentDiscountAmount);

            return value;
        }

        public decimal GetTotalAfterDiscount(BenefitsSummary data)
        {
            var value = 0M;

            value = data.TotalCostBeforeDiscount - data.TotalDiscountAmount;

            return value;
        }
    }
}
