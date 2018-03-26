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
using EmployeeBenefits.Business.Discounts;

namespace EmployeeBenefits.Tests.Business.Discounts
{
    [TestFixture]
    public class LetterTests : ContextSpecification
    {
        protected Letter sut;
        protected decimal results;

        protected override void Context()
        {
            base.Context();

            sut = new Letter();
        }
    }

    public class WhenLetterDiscountIsCalled : LetterTests
    {
        protected override void BecauseOf()
        {
            results = Letter.SumDiscount("Arnold", "A", .01M);
        }
        [Test]
        public void ItShouldReturnDiscountAmount()
        {
            results.ShouldBeEquivalentTo(.01M);
        }
    }
}
