using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.CustomerAddress
{
    public class CustomerAddressUpdateRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
    }
}
