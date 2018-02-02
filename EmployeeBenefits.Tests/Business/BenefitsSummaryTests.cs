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

namespace EmployeeBenefits.Tests.Business
{
    [TestFixture]
    public class BenefitsSummaryTests : ContextSpecification
    {
        protected override void Context()
        {
            base.Context();
        }
    }

    public class WhenBenefitsSummaryIsCalled : BenefitsSummaryTests
    {
        [Test]
        public void ItShouldReturnBenefitsSummary()
        {

        }
    }
}
