using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Product
{
    public class GetProductDeatilRequest : IRequest<Entities.Product>
    {
        public int? Id { get; set; }
    }
}