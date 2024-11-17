using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class CreateOrderRequest : IRequest<ApiResponse>
    {
        public int? CustomerId { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; } = new List<OrderItemDTO>();
        public string? CouponCode { get; set; }
        public int? CustomerAddressId { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
