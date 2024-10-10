namespace NhaThuoc.Domain.ReQuest.Cart
{
    public class CartItemVm
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }

        public string CartId { get; set; }

        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

    }
}
