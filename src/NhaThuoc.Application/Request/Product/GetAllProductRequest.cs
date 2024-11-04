using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Product
{
    public class GetAllProductRequest : IRequest<List<Entities.Product>>
    {
    }
}
