using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Product
{
    public class CreateProductRequest : IRequest<ApiResponse>
    {
        public string? ProductName { get; set; }
        public double? RegularPrice { get; set; }
        public double? DiscountPrice { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Packaging { get; set; }
        public string? Origin { get; set; }
        public string? Manufacturer { get; set; }
        public string? Ingredients { get; set; }
        public string? ImageData { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoAlias { get; set; }
        public bool? IsActived { get; set; } = false;

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public ICollection<int>? CategoryIds { get; set; } = new List<int>();
    }
}