namespace EmployeeBenefits.Data.Entities
{
    public class Promotions
    {
        public int Id { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTrigger { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
