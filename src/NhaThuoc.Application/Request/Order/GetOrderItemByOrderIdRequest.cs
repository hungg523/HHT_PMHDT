using MediatR;
using Entities = NhaThuoc.Domain.Entities;
namespace NhaThuoc.Application.Request.Order
{
    public class GetOrderItemByOrderIdRequest : IRequest<List<Entities.OrderItem>>
    {
        public int? OrderId { get; set; }
    }
}
