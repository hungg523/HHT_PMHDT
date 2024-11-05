using MediatR;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Request.Customers.CustomerAddress
{
    public class CustomerAddressCreateRequest : IRequest<ApiResponse>
    {
        public int? CustomerId { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
    }
}
