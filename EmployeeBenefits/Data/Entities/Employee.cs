namespace EmployeeBenefits.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int NumberOfPayPeriods { get; set; }
    }
}
