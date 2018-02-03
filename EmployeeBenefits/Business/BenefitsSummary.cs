using EmployeeBenefits.Data.Entities;
using System.Collections.Generic;

namespace EmployeeBenefits.Business
{
    public class BenefitsSummary
    {
        public int Id { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeFullName { get; set; }
        public string DiscountTrigger { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal EmployeeSalary { get; set; }
        public decimal EmployeeCostOfBenefits { get; set; }
        public decimal DependentCostOfBenefits { get; set; }
        public decimal DependentCostBeforeDiscount { get; set; }
        public decimal TotalCostBeforeDiscount { get; set; }
        public decimal EmployeeDiscountAmount { get; set; }
        public decimal DependentDiscountAmount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal BiWeeklyCost { get; set; }
        public List<Dependent> DependentsList { get; set; }
        public List<Promotions> PromotionsList { get; set; }
    }
}
