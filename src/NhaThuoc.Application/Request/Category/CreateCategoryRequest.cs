using MediatR;
using NhaThuoc.Application.Exceptions;

namespace NhaThuoc.Application.Request.Category
{
    public class CreateCategoryRequest : IRequest<ApiResponse>
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}