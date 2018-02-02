namespace EmployeeBenefits.Data.Entities
{
    public class Benefit
    {
        public int Id { get; set; }
        public decimal EmployeeCost { get; set; }
        public decimal DependentCost { get; set; }
    }
}
