using NhaThuoc.Domain.Abtractions.Common;

namespace NhaThuoc.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
    }
}