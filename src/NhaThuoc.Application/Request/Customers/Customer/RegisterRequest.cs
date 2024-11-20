using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class RegisterRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public string? FirstName { get; set; } = "Chưa cập nhật...";

        [JsonIgnore]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}