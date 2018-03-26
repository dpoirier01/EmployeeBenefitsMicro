using EmployeeBenefits.Data.Entities;
using EmployeeBenefits.Queries.Results;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeBenefits.Business
{
    public class SummarizeBenefits : ISummarizeBenefits
    {
        private readonly IDeterminePromotions _determinePromotions;

        public SummarizeBenefits(IDeterminePromotions determinePromotions)
        {
            _determinePromotions = determinePromotions;
        }

        public BenefitsSummary Run(GetBenefitsDataResults data)
        {
            var results = new BenefitsSummary()
            {
                EmployeeFirstName = data.Employee.FirstName,
                EmployeeLastName = data.Employee.LastName,
                EmployeeCostOfBenefits = data.Benefit.EmployeeCost,
                DependentsList = data.Dependent,
                PromotionsList = data.Promotions,
                DependentCostOfBenefits = data.Benefit.DependentCost,
                EmployeeFullName = data.Employee.FirstName + " " + data.Employee.LastName,
                DiscountAmount = data.Promotions.Select(x => x.DiscountAmount).FirstOrDefault()
            };

            if (data.Promotions != null)
                results.DiscountTrigger = data.Promotions.Select(x => x.PromotionTrigger).FirstOrDefault();

            var currentPromotions = _determinePromotions.Run(results.PromotionsList);
           
            results.EmployeeDiscountAmount = currentPromotions.Select(x => x.LetterPromo.GetDiscount(results.EmployeeLastName, results.DiscountTrigger, results.DiscountAmount)).FirstOrDefault();



            if (data.Promotions != null)
                results.DiscountAmount = data.Promotions.Select(x => x.DiscountAmount).FirstOrDefault();

            results.EmployeeFullName = GetEmployeeFullName(data);
            results.DependentCostBeforeDiscount = GetDependentCostBeforeDiscount(data);
            results.TotalCostBeforeDiscount = GetTotalBeforeDiscount(results);

            //results.EmployeeDiscountAmount = GetLastNameDiscountAmount(results.EmployeeLastName, results.PromotionsList);

            if (results.DependentsList != null)
                results.DependentDiscountAmount = GetLastNameDiscountAmount(results.DependentsList.Select(x => x.LastName), results.PromotionsList);

            results.CalculatedEmployeeDiscount = CalculateDiscountAmount(results.EmployeeCostOfBenefits, results.EmployeeDiscountAmount);
            results.CalculatedDependentDiscount = CalculateDiscountAmount(results.DependentCostOfBenefits, results.DependentDiscountAmount);

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

            if (data.Dependent == null || !data.Dependent.Any())
                return value;

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

        public decimal CalculateDiscountAmount(decimal costOfBenefits, decimal discountAmount)
        {
            var value = 0M;

            value = costOfBenefits * discountAmount;

            return value;
        }
        
        public decimal GetLastNameDiscountAmount(string lastName, List<Promotions> PromotionsList)
        {
            var value = 0M;

            if (PromotionsList == null || !PromotionsList.Any())
                return value;

            foreach (var promo in PromotionsList)
            {
                if (lastName.StartsWith(promo.PromotionTrigger))
                {
                    value += promo.DiscountAmount;
                }
            }
            return value;
        }

        public decimal GetLastNameDiscountAmount(IEnumerable<string> lastNames, List<Promotions> PromotionsList)
        {
            var value = 0M;

            if (PromotionsList == null || !PromotionsList.Any())
                return value;

            foreach (var promo in PromotionsList)
            {
                foreach (var lastName in lastNames)
                {
                    if (lastName.StartsWith(promo.PromotionTrigger))
                    {
                        value += promo.DiscountAmount;
                    }
                }
            }
            return value;
        }

        public decimal GetTotalDiscountAmount(BenefitsSummary data)
        {
            var value = 0M;

            value = (data.CalculatedEmployeeDiscount + data.CalculatedDependentDiscount);

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
