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

        public BenefitsContext(DbContextOptions<BenefitsContext> options) : base(options)
        { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Dependent> Dependent { get; set; }
        public DbSet<Benefit> Benefit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
            }
        }
    }
}
