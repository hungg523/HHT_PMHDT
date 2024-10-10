namespace NhaThuoc.Domain.ReQuest.Coupon
{
    public class CouponVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CouponDescription { get; set; }
        public int TimesUsed { get; set; }
        public int MaxUsage { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsActive { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
    
    }
}
