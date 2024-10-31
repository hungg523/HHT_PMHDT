using MediatR;
using NhaThuoc.Application.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Category
{
    public class UpdateCategoryRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public bool? IsActive { get; set; }
    }
}
