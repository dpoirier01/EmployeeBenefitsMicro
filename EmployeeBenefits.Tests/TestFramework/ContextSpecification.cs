using EmployeeBenefits.Data;
using EmployeeBenefits.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EmployeeBenefits.Tests.TestFramework
{
    public abstract class ContextSpecification
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {

            get { return this.testContextInstance; }

            set { this.testContextInstance = value; }

        }

        [SetUp]
        public void TestInitialize()
        {

            this.Context();

            this.BecauseOf();

        }

        [TearDown]
        public void TestCleanup()
        {

            this.Cleanup();

        }

        protected virtual void Context()
        {

        }

        protected virtual void BecauseOf()
        {

        }

        protected virtual BenefitsContext BenefitsDatabase()
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<BenefitsContext>()
                .UseSqlite(connection)
                .Options;

            var context = new BenefitsContext(options);

            context.Database.EnsureCreated();

            context.Employee.Add(new Employee { Id = 2, FirstName = "David", LastName = "Arnison", NumberOfPayPeriods = 52, Salary = 100000 });
            context.Employee.Add(new Employee { Id = 3, FirstName = "Brian", LastName = "Smith", NumberOfPayPeriods = 54, Salary = 92000 });
            context.Dependent.Add(new Dependent { Id = 1, FirstName = "John", LastName = "Arnison", Relationship = "Son", EmployeeId = 2 });
            context.Dependent.Add(new Dependent { Id = 2, FirstName = "Jamie", LastName = "Taylor", Relationship = "Daughter", EmployeeId = 2 });
            context.Dependent.Add(new Dependent { Id = 3, FirstName = "Bob", LastName = "Arnison", Relationship = "Son", EmployeeId = 2 });
            context.Benefit.Add(new Benefit { Id = 1, EmployeeCost = 1000, DependentCost = 500 });
            context.Promotions.Add(new Promotions { Id = 1, PromotionName = "Name", PromotionTrigger = "A", DiscountAmount = 0.1M });
            context.SaveChanges();

            return context;
        }

        protected virtual void Cleanup()
        {

        }
    }
}
