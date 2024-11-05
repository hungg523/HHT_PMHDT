using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Product
{
    public class GetProductByCategoryIdRequest : IRequest<List<Entities.Product>>
    {
        public int? CategoryId { get; set; }
    }
}
