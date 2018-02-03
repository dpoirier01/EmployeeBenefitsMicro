using EmployeeBenefits.Queries.Handlers;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Tests.TestFramework;
using NUnit.Framework;
using FluentAssertions;
using EmployeeBenefits.Queries.Results;
using EmployeeBenefits.Data;

namespace EmployeeBenefits.Tests.Queries.Handlers
{

    [TestFixture]
    public class GetBenefitsDataHandlerTests : ContextSpecification
    {
        protected GetBenefitsDataHandler sut;
        protected GetBenefitsDataResults results;
        protected GetBenefitsDataMessage message;

        protected override void Context()
        {
            base.Context();

            var context = BenefitsDatabase();

            sut = new GetBenefitsDataHandler(context);

            message = new GetBenefitsDataMessage { EmployeeId = 2 };
        }

        protected override BenefitsContext BenefitsDatabase()
        {
            return base.BenefitsDatabase();
        }

        protected override void BecauseOf()
        {
            results = sut.Handle(message);
        }
    }

    public class WhenGetBenefitsSummaryHandlerIsCalledWithEmployee : GetBenefitsDataHandlerTests
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

        [Test]
        public void ItShouldReturnPromotions()
        {
            results.Promotions.Should().NotBeNull();
        }
    }
}
