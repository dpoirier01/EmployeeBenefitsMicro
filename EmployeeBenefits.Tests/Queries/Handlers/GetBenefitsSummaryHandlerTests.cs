using EmployeeBenefits.Queries.Handlers;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Tests.TestFramework;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeBenefits.Queries.Results;
using EmployeeBenefits.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using EmployeeBenefits.Data.Entities;
using Microsoft.Data.Sqlite;

namespace EmployeeBenefits.Tests.Queries.Handlers
{

    [TestFixture]
    public class GetBenefitsSummaryHandlerTests : ContextSpecification
    {
        protected GetBenefitsDataHandler sut;
        protected GetBenefitsDataResults results;

        protected override void Context()
        {
            base.Context();

            var context = this.GetBenefitsData();

            sut = new GetBenefitsDataHandler(context);

            var message = new GetBenefitsDataMessage { EmployeeId = 2 };

            results = sut.Handle(message);
        }

        protected BenefitsContext GetBenefitsData()
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<BenefitsContext>()
                .UseSqlite(connection)
                .Options;

            var context = new BenefitsContext(options);
          
            context.Database.EnsureCreated();

            context.Employee.Add(new Employee { Id = 2, FirstName = "David", LastName = "Taylor", NumberOfPayPeriods = 52, Salary = 100000 });
            context.Dependent.Add(new Dependent { Id = 1, FirstName = "John", LastName = "Arnison", Relationship = "Son", EmployeeId = 2 });
            context.Dependent.Add(new Dependent { Id = 2, FirstName = "Jamie", LastName = "Taylor", Relationship = "Daughter", EmployeeId = 2 });
            context.Benefit.Add(new Benefit { Id = 1, EmployeeCost = 1000, DependentCost = 500 });
            context.SaveChanges();

            return context;
        }
    }

    public class WhenGetBenefitsSummaryHandlerIsCalledWithEmployee : GetBenefitsSummaryHandlerTests
    {
        [Test]
        public void ItShouldReturnCorrectType()
        {
            results.Should().BeOfType<GetBenefitsDataResults>();
        }

        [Test]
        public void ItShouldReturnEmployee()
        {
            results.Employee.Should().NotBeNull();
        }

        [Test]
        public void ItShouldReturnListOfDependents()
        {
            results.Dependent.Should().NotBeNull();
        }

        [Test]
        public void ItShouldReturnBenefits()
        {
            results.Benefit.Should().NotBeNull();
        }
    }
}
