using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.CustomerAddress
{
    public class GetByIdCustomerAddressRequestHandler : IRequestHandler<GetByIdCustomerAddressRequest, Entities.CustomerAddress>
    {
        private readonly ICustomerAddressRepository customerAddressRepository;
        private readonly IMapper mapper;

        public GetByIdCustomerAddressRequestHandler(ICustomerAddressRepository customerAddressRepository, IMapper mapper)
        {
            this.customerAddressRepository = customerAddressRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.CustomerAddress> Handle(GetByIdCustomerAddressRequest request, CancellationToken cancellationToken)
        {
            var customeraddress = await customerAddressRepository.FindByIdAsync(request.Id);
            if (customeraddress is null) customeraddress.ThrowNotFound();
            return mapper.Map<Domain.Entities.CustomerAddress>(customeraddress);
        }
    }
}