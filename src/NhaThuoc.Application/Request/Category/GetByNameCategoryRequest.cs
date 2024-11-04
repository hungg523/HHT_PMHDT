using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Category
{
    public class GetByNameCategoryRequest : IRequest<Entities.Category>
    {
        public int? Id { get; set; }
    }
}
