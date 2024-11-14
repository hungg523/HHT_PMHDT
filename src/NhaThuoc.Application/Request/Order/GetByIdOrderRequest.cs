using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Order
{
    public class GetByIdOrderRequest : IRequest<Entities.Order>
    {
        public int? Id { get; set; }
    }
}
