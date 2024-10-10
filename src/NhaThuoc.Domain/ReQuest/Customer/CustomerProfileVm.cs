using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.ReQuest.Customer
{
    public class CustomerProfileVm
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
         
        public List<CustomerAddress>?  Addresses { get; set; }
        public List<OrderItem>? ListOrder { get; set; }

    }
}
