using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Category
{
    public class CreateCategoryRequest : IRequest<ApiResponse>
    {
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageData { get; set; }
        public bool? IsActive { get; set; } = false;
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}