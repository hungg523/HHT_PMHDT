using NhaThuoc.Domain.Abtractions.Common;

namespace NhaThuoc.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}