namespace NhaThuoc.Domain.ReQuest.Order
{
    public class OrderItemVm
    {
        
        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
