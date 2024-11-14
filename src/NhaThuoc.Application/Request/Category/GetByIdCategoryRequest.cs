using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Category
{
    public class GetByIdCategoryRequest : IRequest<Entities.Category>
    {
        public int? Id { get; set; }
    }
}
