namespace NhaThuoc.Application.Request.Customer
{
    public class CustomerAddressCreateRequest
    {
        public int CustomerId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
