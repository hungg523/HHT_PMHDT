using MediatR;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class LoginRequest : IRequest<ApiResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
      //  public bool? RememberMe { get; set; }
    }
}
