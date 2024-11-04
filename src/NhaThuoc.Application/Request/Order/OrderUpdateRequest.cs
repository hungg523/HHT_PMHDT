using NhaThuoc.Share.Enums;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Order
{
    public class OrderUpdateRequest
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? CouponId { get; set; }
        public int? CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
