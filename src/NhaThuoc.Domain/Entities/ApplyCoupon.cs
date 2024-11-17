using NhaThuoc.Domain.Abtractions.Common;

namespace NhaThuoc.Domain.Entities
{
    public class ApplyCoupon : BaseEntity
    {
        public int? CouponId { get; set; }
        public int? OrderId { get; set; }
    }
}