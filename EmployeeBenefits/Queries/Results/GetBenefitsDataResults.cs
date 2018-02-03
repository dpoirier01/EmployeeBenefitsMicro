using EmployeeBenefits.Data;
using EmployeeBenefits.Data.Entities;
using System.Collections.Generic;

namespace EmployeeBenefits.Queries.Results
{
    public class GetBenefitsDataResults
    {
        public Employee Employee { get; set; }
        public List<Dependent> Dependent { get; set; }
        public Benefit Benefit { get; set; }
        public List<Promotions> Promotions { get; set; }
    }
}
