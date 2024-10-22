using MediatR;
using NhaThuoc.Domain.Exceptions;

namespace NhaThuoc.Application.Request.Category
{
    public class CreateCategoryRequest : IRequest<NhaThuocException>
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}