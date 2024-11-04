using MediatR;

namespace NhaThuoc.Application.Request.Category
{
    public class GetByNameCategory : IRequest<Category>
    {
        public int? Id { get; set; }
    }
}
