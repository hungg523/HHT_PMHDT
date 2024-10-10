using NhaThuoc.Domain.ReQuest.Customer;

namespace NhaThuoc.Domain.ReQuest.Order
{
    public class OrderItemCustomerAddress
    {
        public List<CustomerAddressVm> CustomerAddressVm { get; set; }

        public List<OrderItemVm> OrderItemVm { get; set; }
    }
}
