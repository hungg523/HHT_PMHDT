using MediatR;
using NhaThuoc.Share.Exceptions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Customers.Customer
{
    public class GetCustomerByEmailRequest : IRequest<Entities.Customer>
    {
        public string? Email { get; set; }
    }
}
