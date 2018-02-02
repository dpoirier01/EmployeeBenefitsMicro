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
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Queries.Results;
using System.Threading.Tasks;
using System.Threading;

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
        public void ItShouldCallGetSummary()
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

            A.CallTo(() => mediator.Send(A<GetBenefitsDataMessage>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappened();
        }
    }

    public class WhenBenefitsSummaryIsCalledWithoutValidEmployee : EmployeeModuleTests
    {
        [Test]
        public void ItShouldReturnBadRequest()
        {
            var browser = new Browser(with =>
            {
                with.Module<EmployeeBenefits.Service.EmployeeModule>();
                with.Dependencies(mediator);
            });

            var response = browser.Get("/benefitssummary/0");

            response.Result.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }
    }
}
