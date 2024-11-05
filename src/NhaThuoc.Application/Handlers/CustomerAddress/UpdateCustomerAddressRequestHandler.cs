using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Application.Validators.CustomerAddress;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.CustomerAddress
{
    public class UpdateCustomerAddressRequestHandler : IRequestHandler<CustomerAddressUpdateRequest, ApiResponse>
    {
        private readonly ICustomerAddressRepository customerAddressRepository;
        private readonly IMapper mapper;

        public UpdateCustomerAddressRequestHandler(ICustomerAddressRepository customerAddressRepository, IMapper mapper)
        {
            this.customerAddressRepository = customerAddressRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(CustomerAddressUpdateRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerAddressRepository.BeginTransaction())
            {
                try
                {
                    var validator = new UpdateCustomerAddressRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customeraddress = await customerAddressRepository.FindByIdAsync(request.Id!);
                    if (customeraddress is null) customeraddress.ThrowNotFound();
                    customeraddress.CustomerId = request.CustomerId ?? customeraddress.CustomerId;
                    customeraddress.Address = request.Address ?? customeraddress.Address;
                    customeraddress.FullName = request.FullName ?? customeraddress.FullName;
                    customeraddress.Phone = request.Phone ?? customeraddress.Phone;
                    customeraddress.Province = request.Province ?? customeraddress.Province;
                    customeraddress.District = request.District ?? customeraddress.District;
                    customeraddress.Ward = request.Ward ?? customeraddress.Ward;


                    customerAddressRepository.Update(customeraddress);
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
