using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class OrderCreateRequest
    {
        public int? CustomerId { get; set; }


        //public OrderDetail OrderDetails { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        //public int? CouponId { get; set; }


    }

}
