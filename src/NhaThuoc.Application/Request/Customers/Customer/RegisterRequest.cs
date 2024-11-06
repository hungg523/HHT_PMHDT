using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class RegisterRequest : IRequest<ApiResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        [JsonIgnore]
        public int? Role { get; set; } = 0;
    }
}
