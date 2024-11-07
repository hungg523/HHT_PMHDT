using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class GetAllCustomerRequestHandler : IRequestHandler<GetAllCustomerRequest, List<Entities.Customer>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public GetAllCustomerRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<List<Entities.Customer>> Handle(GetAllCustomerRequest request, CancellationToken cancellationToken)
        {
            var customers = await customerRepository
                                 .FindAll(u => u.Role == 0)
                                 .ToListAsync(cancellationToken);

            return mapper.Map<List<Entities.Customer>>(customers);
        }
    }
}
