using MediatR;
using NhaThuoc.Application.DTOs;

namespace NhaThuoc.Application.Request.Order
{
    public class GetOrderByCustomerIdRequest : IRequest<List<OrderDTO>>
    {
        public int Id { get; set; }
    }
}