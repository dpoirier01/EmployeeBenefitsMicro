﻿using System.Collections.Generic;

namespace EmployeeBenefits.Queries.Results
{
    public class GetBenefitsSummaryResults
    {
        public string EmployeeName { get; set; }
        public decimal EmployeeSalary { get; set; }
        public decimal EmployeeCost { get; set; }
        public decimal AnnualTotal { get; set; }
        public decimal BiWeeklyTotal { get; set; }
        public decimal Savings { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<DependentSummary> DependentSummaryList { get; set; }
    }

    public class DependentSummary
    {
        public string DependentName { get; set; }
        public string Relationship { get; set; }
        public decimal DependentCost { get; set; }
        public bool DiscountFlag { get; set; }
    }
}
