using MediatR;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Order
{
    public class OrderUpdateRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public OrderStatus? Status { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
