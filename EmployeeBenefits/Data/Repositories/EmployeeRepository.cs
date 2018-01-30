using EmployeeBenefits.Data.Entities;

namespace EmployeeBenefits.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository 
    {
        public EmployeeRepository(BenefitsContext dbContext) : base(dbContext) { }
    }
}
