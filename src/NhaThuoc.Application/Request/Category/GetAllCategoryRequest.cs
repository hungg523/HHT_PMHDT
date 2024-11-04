using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Category
{
    public class GetAllCategoryRequest : IRequest<List<Entities.Category>>
    {
    }
}
