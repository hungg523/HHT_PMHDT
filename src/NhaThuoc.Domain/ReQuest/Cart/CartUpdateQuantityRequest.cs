namespace NhaThuoc.Domain.ReQuest.Cart
{
    public class CartUpdateQuantityRequest
    {

        public int? CustomerId { get; set; }

        public string CartId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
