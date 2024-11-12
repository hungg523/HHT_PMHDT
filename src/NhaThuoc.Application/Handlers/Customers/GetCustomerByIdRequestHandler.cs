using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdCustomerRequest, Entities.Customer>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public GetCustomerByIdRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<Entities.Customer> Handle(GetCustomerByIdCustomerRequest request, CancellationToken cancellationToken)
        {
            var customers = await customerRepository.FindByIdAsync(request.Id);
            return mapper.Map<Entities.Customer>(customers);
        }
    }
}
