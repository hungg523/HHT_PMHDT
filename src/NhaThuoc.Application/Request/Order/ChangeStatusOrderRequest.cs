using MediatR;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Order
{
    public class ChangeStatusOrderRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
