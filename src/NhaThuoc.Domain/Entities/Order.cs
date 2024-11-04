using NhaThuoc.Domain.Abtractions.Common;
using NhaThuoc.Domain.Abtractions.Enums;

namespace NhaThuoc.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}