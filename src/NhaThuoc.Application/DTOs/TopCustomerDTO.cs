namespace NhaThuoc.Application.DTOs
{
    public class TopCustomerDTO
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
    }
}