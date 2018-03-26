using EmployeeBenefits.Data.Entities;
using System.Collections.Generic;

namespace EmployeeBenefits.Business
{
    public interface IDeterminePromotions
    {
        List<PromotionTypes> Run(List<Promotions> promotions);
    }
}
