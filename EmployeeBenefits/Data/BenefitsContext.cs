using EmployeeBenefits.Commands.Models;
using EmployeeBenefits.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeBenefits.Data
{
    public class BenefitsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private IConfigurationRoot _config;

        public BenefitsContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
        }
    }
}
