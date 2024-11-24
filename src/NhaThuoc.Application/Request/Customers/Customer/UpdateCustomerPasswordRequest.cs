using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class UpdateCustomerPasswordRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public string? Email { get; set; }
        public string? OTP { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}