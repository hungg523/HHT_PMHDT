using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Application.Validators.CustomerAddress;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Extensions;

namespace NhaThuoc.Application.Handlers.CustomerAddress
{
    public class CreateCustomerAddressRequestHandler : IRequestHandler<CustomerAddressCreateRequest, ApiResponse>
    {
        private readonly ICustomerAddressRepository customerAddressRepository;
        private readonly IMapper mapper;

        public CreateCustomerAddressRequestHandler(ICustomerAddressRepository customerAddressRepository, IMapper mapper)
        {
            this.customerAddressRepository = customerAddressRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(CustomerAddressCreateRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerAddressRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateCustomerAddressRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customeraddress = mapper.Map<Domain.Entities.CustomerAddress>(request);

                    customerAddressRepository.Create(customeraddress);
                    await customerAddressRepository.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return ApiResponse.Success();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
