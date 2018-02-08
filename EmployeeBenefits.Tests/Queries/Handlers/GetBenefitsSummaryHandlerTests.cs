using EmployeeBenefits.Queries.Handlers;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Tests.TestFramework;
using NUnit.Framework;
using FluentAssertions;
using EmployeeBenefits.Queries.Results;
using EmployeeBenefits.Data;
using FakeItEasy;
using EmployeeBenefits.Business;
using MediatR;
using EmployeeBenefits.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using FizzWare.NBuilder;

namespace EmployeeBenefits.Tests.Queries.Handlers
{

    [TestFixture]
    public class GetBenefitsSummaryHandlerTests : ContextSpecification
    {
        protected GetBenefitsSummaryHandler sut;
        protected BenefitsSummary results;
        protected GetBenefitsDataSummary message;
        protected IMediator mediator;

        protected override void Context()
        {
            base.Context();

            mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(A<GetBenefitsDataSummary>.Ignored, A<CancellationToken>.Ignored)).Returns(GetBenefitsSummary());

            sut = new GetBenefitsSummaryHandler(mediator);
        }

        protected BenefitsSummary GetBenefitsSummary()
        {
            var data = Builder<BenefitsSummary>.CreateNew().Build();
            return data;
        }
            

        protected override void BecauseOf()
        {
            message = new GetBenefitsDataSummary { EmployeeId = 2 };

            results = sut.Handle(message);
        }
    }

    public class WhenGetBenefitsSummaryHandlerIsCalledWithEmployee : GetBenefitsSummaryHandlerTests
    {
        [Test]
        public void ItShouldReturnCorrectType()
        {
            results.Should().BeOfType<BenefitsSummary>();
        }

        [Test]
        public void ItShouldNotReturnNull()
        {
            results.Should().NotBeNull();
        }
    }
}
