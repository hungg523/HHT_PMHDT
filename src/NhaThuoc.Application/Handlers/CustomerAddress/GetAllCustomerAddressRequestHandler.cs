using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.CustomerAddress
{
    public class GetAllCustomerAddressRequestHandler : IRequestHandler<GetAllCustomerAddressRequest, List<Entities.CustomerAddress>>
    {
        private readonly ICustomerAddressRepository customerAddressRepository;
        private readonly IMapper mapper;

        public GetAllCustomerAddressRequestHandler(ICustomerAddressRepository customerAddressRepository, IMapper mapper)
        {
            this.customerAddressRepository = customerAddressRepository;
            this.mapper = mapper;
        }

        public async Task<List<Entities.CustomerAddress>> Handle(GetAllCustomerAddressRequest request, CancellationToken cancellationToken)
        {
            var customeraddresses = customerAddressRepository.FindAll();

            return mapper.Map<List<Entities.CustomerAddress>>(customeraddresses);
        }
    }
}
