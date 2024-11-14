using AutoMapper;
using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Application.Validators.CustomerAddress;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;

namespace NhaThuoc.Application.Handlers.CustomerAddress
{
    public class GetCustomerAddressByCustomerIdRequestHandler : IRequestHandler<GetCustomerAddressByCustomerIdRequest, List<CustomerAddressDTO>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerAddressRepository customerAddressRepository;
        private readonly IMapper mapper;

        public GetCustomerAddressByCustomerIdRequestHandler(ICustomerRepository customerRepository, ICustomerAddressRepository customerAddressRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.customerAddressRepository = customerAddressRepository;
            this.mapper = mapper;
        }

        public async Task<List<CustomerAddressDTO>> Handle(GetCustomerAddressByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            var validator = new GetCustomerAddressByCustomerIdRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            validationResult.ThrowIfInvalid();

            var customer = await customerRepository.FindByIdAsync(request.CustomerId);
            if (customer is null) customer.ThrowNotFound();

            var customeAaddress = customerAddressRepository.FindAll(x => x.CustomerId == request.CustomerId).ToList();
            if (customeAaddress is null) customeAaddress.ThrowNotFound();
            
            return mapper.Map<List<CustomerAddressDTO>>(customeAaddress);
        }
    }
}