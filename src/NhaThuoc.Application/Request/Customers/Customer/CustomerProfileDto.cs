using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;
using Entities= NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class CustomerProfileDto : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public List<Entities.CustomerAddress>? Addresses { get; set; }
        [JsonIgnore]
        public List<Entities.OrderItem>? ListOrder { get; set; }

    }
}
