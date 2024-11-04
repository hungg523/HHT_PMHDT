using Entities = NhaThuoc.Domain.Entities;
namespace NhaThuoc.Application.Request.Customers.CustomerAddress
{
    public class CustomerAddressVm
    {
        public int CustomerId { get; set; }
        public List<Entities.CustomerAddress> Addresses { get; set; }
    }
}
