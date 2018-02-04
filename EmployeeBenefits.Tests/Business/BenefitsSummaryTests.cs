using EmployeeBenefits.Tests.TestFramework;
using NUnit.Framework;
using FluentAssertions;
using EmployeeBenefits.Queries.Results;
using EmployeeBenefits.Data;
using FakeItEasy;
using EmployeeBenefits.Business;
using System.Linq;
using EmployeeBenefits.Framework.Tasks;

namespace EmployeeBenefits.Tests.Business
{
    [TestFixture]
    public class BenefitsSummaryTests : ContextSpecification
    {
        protected SummarizeBenefits sut;
        protected BenefitsSummary results;
        protected IProcess<BenefitsSummary> process;

        protected override void Context()
        {
            base.Context();

            process = A.Fake<IProcess<BenefitsSummary>>();

            sut = new SummarizeBenefits(process);
        }

        protected override BenefitsContext BenefitsDatabase()
        {
            return base.BenefitsDatabase();
        }

        protected GetBenefitsDataResults GetBenefitsDataWithEmployeeDiscount()
        {
            var data = this.BenefitsDatabase();

            var benefitsData = new GetBenefitsDataResults();
            benefitsData.Employee = data.Employee.Where(x => x.Id == 2).FirstOrDefault();
            benefitsData.Dependent = data.Dependent.Where(x => x.EmployeeId == 2).ToList();
            benefitsData.Benefit = data.Benefit.FirstOrDefault();
            benefitsData.Promotions = data.Promotions.ToList();

            return benefitsData;
        }

        protected GetBenefitsDataResults GetBenefitsDataWithoutEmployeeDiscount()
        {
            var data = this.BenefitsDatabase();

            var benefitsData = new GetBenefitsDataResults();
            benefitsData.Employee = data.Employee.Where(x => x.Id == 3).FirstOrDefault();
            benefitsData.Dependent = data.Dependent.Where(x => x.EmployeeId == 3).ToList();
            benefitsData.Benefit = data.Benefit.FirstOrDefault();
            benefitsData.Promotions = data.Promotions.ToList();

            return benefitsData;
        }
    }

    public class WhenBenefitsSummaryRunIsCalledWithDiscounts : BenefitsSummaryTests
    {
        protected override void BecauseOf()
        {
            results = sut.Run(this.GetBenefitsDataWithEmployeeDiscount());
        }

        [Test]
        public void ItShouldReturnBenefitsSummary()
        {
            results.Should().BeOfType<BenefitsSummary>();
        }

        [Test]
        public void ItShouldReturnEmployeeName()
        {
            results.EmployeeFullName.ShouldBeEquivalentTo("David Arnison");
        }

        [Test]
        public void ItShouldReturnEmployeeCost()
        {
            results.EmployeeCostOfBenefits.ShouldBeEquivalentTo(1000);
        }

        [Test]
        public void ItShouldReturnDependentCostBeforeDiscount()
        {
            results.DependentCostBeforeDiscount.ShouldBeEquivalentTo(1500);
        }

        [Test]
        public void ItShouldReturnTotalBeforeDiscount()
        {
            results.TotalCostBeforeDiscount.ShouldBeEquivalentTo(2500);
        }

        [Test]
        public void ItShouldReturnEmployeeDiscountAmount()
        {
            results.EmployeeDiscountAmount.ShouldBeEquivalentTo(100);
        }

        [Test]
        public void ItShouldReturnDependentDiscountAmount()
        {
            results.DependentDiscountAmount.ShouldBeEquivalentTo(100);
        }

        [Test]
        public void ItShouldReturnTotalDiscountAmount()
        {
            results.TotalDiscountAmount.ShouldBeEquivalentTo(200);
        }

        [Test]
        public void ItShouldReturnTotalAfterDiscount()
        {
            results.TotalAfterDiscount.ShouldBeEquivalentTo(2300);
        }
    }

    public class WhenBenefitsSummaryRunIsCalledWithoutDiscounts : BenefitsSummaryTests
    {
        protected override void BecauseOf()
        {
            results = sut.Run(this.GetBenefitsDataWithoutEmployeeDiscount());
        }

        [Test]
        public void ItShouldReturnEmployeeDiscountOfZero()
        {
            results.EmployeeDiscountAmount.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void ItShouldReturnDependentDiscountOfZero()
        {
            results.EmployeeDiscountAmount.ShouldBeEquivalentTo(0);
        }
    }
}
