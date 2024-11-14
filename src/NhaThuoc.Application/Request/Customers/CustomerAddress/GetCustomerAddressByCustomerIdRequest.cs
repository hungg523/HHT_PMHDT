using MediatR;
using NhaThuoc.Application.DTOs;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Customers.CustomerAddress
{
    public class GetCustomerAddressByCustomerIdRequest : IRequest<List<CustomerAddressDTO>>
    {
        [JsonIgnore]
        public int? CustomerId { get; set; }
    }
}