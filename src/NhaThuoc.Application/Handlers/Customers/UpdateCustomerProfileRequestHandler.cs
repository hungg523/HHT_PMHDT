using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class UpdateCustomerProfileRequestHandler : IRequestHandler<CustomerProfileDto, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public UpdateCustomerProfileRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(CustomerProfileDto request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CustomerProfileValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customer = await customerRepository.FindByIdAsync(request.Id!);
                    if (customer is null) customer.ThrowNotFound();
                    customer.FirstName = request.FirstName ?? customer.FirstName;
                    customer.LastName = request.LastName ?? customer.LastName;
                    customer.PhoneNumber = request.PhoneNumber ?? customer.PhoneNumber;
                    customer.AvatarImagePath = request.AvatarImagePath ?? customer.AvatarImagePath;
                    customer.Email = request.Email ?? customer.Email;

                    customerRepository.Update(customer);
                    await customerRepository.SaveChangesAsync();
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
