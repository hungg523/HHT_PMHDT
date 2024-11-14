namespace NhaThuoc.Application.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public CustomerAddressDTO? Address { get; set; }
        public ICollection<OrderItemDTO>? OrderItems { get; set; }
    }
}