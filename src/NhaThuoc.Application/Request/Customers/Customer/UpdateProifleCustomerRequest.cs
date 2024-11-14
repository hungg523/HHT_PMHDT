using MediatR;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class UpdateProifleCustomerRequest : IRequest<ApiResponse>
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? ImageData { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
    }
}