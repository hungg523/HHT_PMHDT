using NhaThuoc.Domain.Enums;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class OrderDetailVm
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
