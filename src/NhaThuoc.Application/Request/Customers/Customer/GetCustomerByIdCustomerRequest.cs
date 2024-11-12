using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class GetCustomerByIdCustomerRequest : IRequest<Entities.Customer>
    {
        public int? Id { get; set; }
    }
}
