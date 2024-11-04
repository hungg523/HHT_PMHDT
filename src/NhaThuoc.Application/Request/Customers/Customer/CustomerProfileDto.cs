using Entities= NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class CustomerProfileDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Entities.CustomerAddress>? Addresses { get; set; }
        public List<Entities.OrderItem>? ListOrder { get; set; }

    }
}
