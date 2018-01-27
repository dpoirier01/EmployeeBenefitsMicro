using EmployeeBenefits.Tests.TestFramework;
using Nancy.Testing;
using NUnit.Framework;
using EmployeeBenefits;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FakeItEasy;
using FluentAssertions;
using Nancy;
using FizzWare.NBuilder;

namespace EmployeeBenefits.Tests.EmployeeModule
{
    [TestFixture]
    public class EmployeeModuleTests : ContextSpecification
    {
        protected IMediator mediator;

        protected override void Context()
        {
            base.Context();

            mediator = A.Fake<IMediator>();
        }

        protected class BenefitsSummary
        {
            protected string EmployeeName { get; set; }
            protected decimal EmployeeSalary { get; set; }
            protected decimal EmployeeCost { get; set; }
            protected decimal AnnualTotal { get; set; }
            protected decimal BiWeeklyTotal { get; set; }
            protected decimal Savings { get; set; }
            protected decimal DiscountAmount { get; set; }
            protected List<DependentSummary> DependentSummary { get; set; }
        }

        protected class DependentSummary
        {
            protected string DependentName { get; set; }
            protected string Relationship { get; set; }
            protected decimal DependentCost { get; set; }
            protected bool DiscountFlag { get; set; }
        }

        protected IList<BenefitsSummary> GetBenefitsSummaryList()
        {
            return Builder<BenefitsSummary>.CreateListOfSize(1).Build();
        }
    }

    public class WhenBenefitsSummaryIsCalledWithEmployee : EmployeeModuleTests
    {

        [Test]
        public void ItShouldReturnOkay()
        {
            var browser = new Browser(with =>
            {
                with.Module<EmployeeBenefits.Service.EmployeeModule>();
                with.Dependencies(mediator);
            });

            var response = browser.Get("/benefitssummary/2");

            response.Result.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Test]
        public void ItShouldReturnSummary()
        {
            var browser = new Browser(with =>
            {
                with.Module<EmployeeBenefits.Service.EmployeeModule>();
                with.Dependencies(mediator);
            });

            var response = browser.Get("/benefitssummary/2", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var result = response.Result.Body.DeserializeJson<BenefitsSummary>();
        }
    }
    public class WhenBenefitsSummaryIsCalledWithoutEmployee : EmployeeModuleTests
    {
        [Test]
        public void ItShouldReturnBadRequest()
        {

        }
    }
}
