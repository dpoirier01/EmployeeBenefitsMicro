using EmployeeBenefits.Tests.TestFramework;
using NUnit.Framework;
using FluentAssertions;
using EmployeeBenefits.Queries.Results;
using EmployeeBenefits.Data;
using FakeItEasy;
using EmployeeBenefits.Business;
using System.Linq;
using EmployeeBenefits.Data.Entities;
using System.Collections.Generic;

namespace EmployeeBenefits.Tests.Business
{
    [TestFixture]
    public class BenefitsSummaryTests : ContextSpecification
    {
        protected SummarizeBenefits sut;
        protected BenefitsSummary results;

        protected override void Context()
        {
            base.Context();
            

            sut = new SummarizeBenefits();
        }

        protected GetBenefitsDataResults GetBenefitsDataWithEmployeeDiscount()
        {
            var benefitsData = new GetBenefitsDataResults();
            
            benefitsData.Employee = new Employee { Id = 2, FirstName = "David", LastName = "Arnison", NumberOfPayPeriods = 52, Salary = 100000 };
            benefitsData.Dependent = new List<Dependent>
            {
                new Dependent { Id = 1, FirstName = "John", LastName = "Arnison", Relationship = "Son", EmployeeId = 2 },
                new Dependent { Id = 2, FirstName = "Jamie", LastName = "Arnison", Relationship = "Daughter", EmployeeId = 2 },
                new Dependent { Id = 2, FirstName = "Jamie", LastName = "Taylor", Relationship = "Daughter", EmployeeId = 2 }
            };
            benefitsData.Benefit = new Benefit { Id = 1, EmployeeCost = 1000, DependentCost = 500 };
            benefitsData.Promotions = new List<Promotions> { new Promotions { Id = 1, PromotionName = "Name", PromotionTrigger = "A", DiscountAmount = 0.1M } };

            return benefitsData;
        }

        protected GetBenefitsDataResults GetBenefitsDataWithoutEmployeeDiscount()
        {
            var benefitsData = new GetBenefitsDataResults();
            
            benefitsData.Employee = new Employee { Id = 3, FirstName = "David", LastName = "Taylor", NumberOfPayPeriods = 52, Salary = 92000 };
            benefitsData.Dependent = new List<Dependent>
            {
                new Dependent { Id = 2, FirstName = "Jamie", LastName = "Taylor", Relationship = "Daughter", EmployeeId = 2 },
                new Dependent { Id = 2, FirstName = "Jamie", LastName = "Taylor", Relationship = "Daughter", EmployeeId = 2 }
            };
            benefitsData.Benefit = new Benefit { Id = 1, EmployeeCost = 1000, DependentCost = 500 };
            benefitsData.Promotions = new List<Promotions> { new Promotions { Id = 1, PromotionName = "Name", PromotionTrigger = "A", DiscountAmount = 0.1M } };

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
