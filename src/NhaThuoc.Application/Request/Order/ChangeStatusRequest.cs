using NhaThuoc.Domain.Abtractions.Enums;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class ChangeStatusRequest
    {
        public int OrderDetailsId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
