using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.CustomerAddress
{
    public class GetByNameCustomerAddressRequest : IRequest<Entities.CustomerAddress>
    {
        public int? Id { get; set; }
    }
}
