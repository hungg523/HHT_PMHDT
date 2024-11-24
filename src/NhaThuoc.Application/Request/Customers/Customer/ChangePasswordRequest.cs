using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class ChangePasswordRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public string? Email { get; set; }
    }
}