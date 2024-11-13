using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class CreateOrderRequest : IRequest<ApiResponse>
    {
        public int CustomerId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
        public string? CouponCode { get; set; }
        public int? CustomerAddressId { get; set; }
        public string? PaymentMethod { get; set; }

        [JsonIgnore]
        public OrderStatus? Status { get; set; } = OrderStatus.Pending;

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
