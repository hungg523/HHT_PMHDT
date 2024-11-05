using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Product
{
    public class GetByNameProductRequest : IRequest<Entities.Product>
    {
        public int? Id { get; set; }
    }
}
