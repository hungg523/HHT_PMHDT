using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class GetAllCustomerRequest : IRequest<List<Entities.Customer>>
    {
    }
}
