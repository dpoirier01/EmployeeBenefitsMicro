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

namespace EmployeeBenefits.Tests.Business
{
    [TestFixture]
    public class DeterminePromotionTests : ContextSpecification
    {
        protected DeterminePromotions sut;
        protected List<PromotionTypes> results;

        protected override void Context()
        {
            base.Context();

            sut = new DeterminePromotions();
        }

        protected List<Promotions> GetLetterPromotions()
        {
            var promotions = new List<Promotions> { new Promotions { Id = 1, PromotionName = "Name", PromotionTrigger = "A", DiscountAmount = 0.1M, DiscountType = "Letter" } };

            return promotions;
        }

        protected List<Promotions> GetNumberOfDependentsPromotions()
        {
            var promotions = new List<Promotions> { new Promotions { Id = 1, PromotionName = "Name", PromotionTrigger = "4", DiscountAmount = 0.15M, DiscountType = "NumDependents" } };

            return promotions;
        }
    }

    public class WhenPassedLetterPromotion : DeterminePromotionTests
    {
        protected override void BecauseOf()
        {
            results = sut.Run(GetLetterPromotions());
        }

        [Test]
        public void ItShouldReturnLetterPromotion()
        {
            var lPromotionType = new List<PromotionTypes>();
            var promoType = new PromotionTypes();

            promoType.LetterPromo = new Letter();

            lPromotionType.Add(promoType);

            results.ShouldBeEquivalentTo(lPromotionType);
        }
    }

    public class WhenPassedNumDependentPromotion : DeterminePromotionTests
    {
        protected override void BecauseOf()
        {
            results = sut.Run(GetNumberOfDependentsPromotions());
        }

        [Test]
        public void ItShouldReturnNumDependentPromotion()
        {
            var lPromotionType = new List<PromotionTypes>();
            var promoType = new PromotionTypes();

            promoType.numberOfDependentsPromo = new NumberOfDependents();

            lPromotionType.Add(promoType);

            results.ShouldBeEquivalentTo(lPromotionType);
        }
    }
}
