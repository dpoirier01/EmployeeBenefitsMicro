﻿using EmployeeBenefits.Queries.Handlers;
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
        protected GetBenefitsSummary message;
        protected IMediator mediator;
        protected ISummarizeBenefits summarizeBenefits;

        protected override void Context()
        {
            base.Context();

            mediator = A.Fake<IMediator>();
            summarizeBenefits = A.Fake<ISummarizeBenefits>();

            A.CallTo(() => mediator.Send(A<GetBenefitsSummary>.Ignored, A<CancellationToken>.Ignored)).Returns(GetBenefitsSummary());

            A.CallTo(() => summarizeBenefits.Run(A<GetBenefitsDataResults>.Ignored)).Returns(GetBenefitsSummary());

            sut = new GetBenefitsSummaryHandler(mediator, summarizeBenefits);
        }

        protected BenefitsSummary GetBenefitsSummary()
        {
            var data = Builder<BenefitsSummary>.CreateNew().Build();
            return data;
        }
    }

    public class WhenGetBenefitsSummaryHandlerIsCalledWithEmployee : GetBenefitsSummaryHandlerTests
    {

        protected override void BecauseOf()
        {
            message = new GetBenefitsSummary { EmployeeId = 2 };

            results = sut.Handle(message);
        }

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
