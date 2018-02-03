using EmployeeBenefits.Tests.TestFramework;
using Nancy.Testing;
using NUnit.Framework;
using EmployeeBenefits;
using System;
using System.Collections.Generic;
using MediatR;
using FakeItEasy;
using FluentAssertions;
using Nancy;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Queries.Results;
using System.Threading.Tasks;
using System.Threading;
using EmployeeBenefits.Business;

namespace EmployeeBenefits.Tests.EmployeeModule
{
    [TestFixture]
    public class EmployeeModuleTests : ContextSpecification
    {
        protected IMediator mediator;
        protected ISummarizeBenefits summarizeBenefits;
        protected Browser browser;
        protected Task<BrowserResponse> response;

        protected override void Context()
        {
            base.Context();

            mediator = A.Fake<IMediator>();
            summarizeBenefits = A.Fake<ISummarizeBenefits>();

            browser = new Browser(with =>
            {
                with.Module<EmployeeBenefits.Service.EmployeeModule>();
                with.Dependencies(mediator);
                with.Dependencies(summarizeBenefits);
            });
        }
    }

    public class WhenBenefitsSummaryIsCalledWithEmployee : EmployeeModuleTests
    {
        protected override void BecauseOf()
        {
            response = browser.Get("/benefitssummary/2", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });
        }

        [Test]
        public void ItShouldReturnOkay()
        {
            response.Result.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Test]
        public void ItShouldCallGetBenefitsData()
        {
            A.CallTo(() => mediator.Send(A<GetBenefitsDataMessage>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void ItShouldCallGetBenefitsSummary()
        {
            A.CallTo(() => summarizeBenefits.Run(A<GetBenefitsDataResults>.Ignored)).MustHaveHappened();
        }
    }

    public class WhenBenefitsSummaryIsCalledWithoutValidEmployee : EmployeeModuleTests
    {
        protected override void BecauseOf()
        {
            response = browser.Get("/benefitssummary/0");
        }

        [Test]
        public void ItShouldReturnBadRequest()
        {
            response.Result.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }
    }
}
