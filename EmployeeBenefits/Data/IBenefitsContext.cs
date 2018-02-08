using EmployeeBenefits.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefits.Data
{
    public interface IBenefitsContext
    {
        DbSet<Employee> Employee { get; set; }
        DbSet<Dependent> Dependent { get; set; }
        DbSet<Benefit> Benefit { get; set; }
        DbSet<Promotions> Promotions { get; set; }
    }
}
