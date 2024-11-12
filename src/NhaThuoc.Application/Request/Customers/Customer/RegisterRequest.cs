using MediatR;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class RegisterRequest : IRequest<ApiResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
