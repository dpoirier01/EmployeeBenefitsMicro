using EmployeeBenefits.Business.Discounts;
using EmployeeBenefits.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Business
{
    public class DeterminePromotions : IDeterminePromotions 
    {
        public List<PromotionTypes> Run(List<Promotions> promotions)
        {

            var lPromotionType = new List<PromotionTypes>();
            var promoType = new PromotionTypes();

            foreach (var promo in promotions)
            {
                if (promo.DiscountType == "Letter")
                {
                    promoType.LetterPromo = new Letter();
                }

                if (promo.DiscountType == "NumDependents")
                {
                    promoType.numberOfDependentsPromo = new NumberOfDependents();
                }
            }
          
            lPromotionType.Add(promoType);

            return lPromotionType;
        }
    }
}
