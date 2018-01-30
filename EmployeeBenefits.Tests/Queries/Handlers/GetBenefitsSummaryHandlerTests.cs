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
using EmployeeBenefits.Data.Repositories;

namespace EmployeeBenefits.Tests.Queries.Handlers
{
    [TestFixture]
    public class GetBenefitsSummaryHandlerTests : ContextSpecification
    {
        protected GetBenefitsSummaryHandler sut;
        protected IBenefitsSummaryRepository db;

        protected override void Context()
        {
            base.Context();

            db = A.Fake<IBenefitsSummaryRepository>();

            sut = new GetBenefitsSummaryHandler(db);
        }
    }

    public class WhenGetBenefitsSummaryHandlerIsCalledWithEmployee : GetBenefitsSummaryHandlerTests
    {
        [Test]
        public void ItShouldReturnCorrectType()
        {
            var message = new GetBenefitsSummaryMessage { EmployeeId = 2 };

            var results = sut.Handle(message);

            results.Should().BeOfType<List<GetBenefitsSummaryResults>>();
        }
    }
}
