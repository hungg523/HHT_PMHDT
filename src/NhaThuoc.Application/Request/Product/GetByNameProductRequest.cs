using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Product
{
    public class GetByNameProductRequest : IRequest<List<Entities.Product>>
    {
        public string? ProductName { get; set; }
    }
}
