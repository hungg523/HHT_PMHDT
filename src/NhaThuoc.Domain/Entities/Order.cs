using NhaThuoc.Domain.Abtractions.Common;
using NhaThuoc.Share.Enums;

namespace NhaThuoc.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerAddressId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}