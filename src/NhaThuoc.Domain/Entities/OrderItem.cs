using NhaThuoc.Domain.Abtractions.Common;
using System.Text.Json.Serialization;

namespace NhaThuoc.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int? Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}