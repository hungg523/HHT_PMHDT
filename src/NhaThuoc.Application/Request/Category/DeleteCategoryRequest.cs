using MediatR;
using NhaThuoc.Application.Exceptions;

namespace NhaThuoc.Application.Request.Category
{
    public class DeleteCategoryRequest : IRequest<ApiResponse>
    {
        public int? Id { get; set; }
    }
}
