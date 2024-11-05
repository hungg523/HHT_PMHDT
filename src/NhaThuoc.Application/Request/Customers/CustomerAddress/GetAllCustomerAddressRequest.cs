using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.CustomerAddress
{
    public class GetAllCustomerAddressRequest : IRequest<List<Entities.CustomerAddress>>
    {
    }
}
