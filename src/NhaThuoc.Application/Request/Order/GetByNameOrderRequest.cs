using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Order
{
    public class GetByNameOrderRequest : IRequest<Entities.Order>
    {
        public int? Id { get; set; }
    }
}
