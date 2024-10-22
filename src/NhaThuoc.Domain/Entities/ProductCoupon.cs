using NhaThuoc.Domain.Abtractions.Common;

namespace NhaThuoc.Domain.Entities
{
    public class ProductCoupon : BaseEntity
    {
        public int CouponId { get; set; }
        public int ProductId { get; set; }
    }
}