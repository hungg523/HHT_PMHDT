using MediatR;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class OrderCreateRequest : IRequest<ApiResponse>
    {
        public int? CustomerId { get; set; }
        public OrderStatus? Status { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
