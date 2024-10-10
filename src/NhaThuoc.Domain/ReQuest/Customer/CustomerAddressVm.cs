using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.ReQuest.Customer
{
    public class CustomerAddressVm
    {
        public int CustomerId {  get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}
