using FluentValidation;
using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class AurhenCustomerRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? OTP { get; set; }
    }
}