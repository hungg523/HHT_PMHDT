using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.CustomerAddress
{
    public class GetByNameCustomerAddressRequestHandler : IRequestHandler<GetByNameCustomerAddressRequest, Entities.CustomerAddress>
    {
        private readonly ICustomerAddressRepository customerAddressRepository;
        private readonly IMapper mapper;

        public GetByNameCustomerAddressRequestHandler(ICustomerAddressRepository customerAddressRepository, IMapper mapper)
        {
            this.customerAddressRepository = customerAddressRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.CustomerAddress> Handle(GetByNameCustomerAddressRequest request, CancellationToken cancellationToken)
        {
            var customeraddress = await customerAddressRepository.FindByIdAsync(request.Id);
            return mapper.Map<Domain.Entities.CustomerAddress>(customeraddress);
        }
    }
}