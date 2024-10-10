namespace NhaThuoc.Domain.ReQuest.Cart
{
    public class CartCreateRequest
    {
        public int? CustomerId { get; set; }
        public string CartId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
