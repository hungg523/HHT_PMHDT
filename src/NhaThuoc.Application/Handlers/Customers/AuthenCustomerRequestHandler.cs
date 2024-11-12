using MediatR;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class AuthenCustomerRequestHandler : IRequestHandler<AuthenCustomerRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;

        public AuthenCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<ApiResponse> Handle(AuthenCustomerRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new AurhenCustomerRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customer = await customerRepository.FindSingleAsync(x => x.Email == request.Email && x.OTP == request.OTP);
                    if (customer is null) customer.ThrowNotFound();

                    if (customer.OTPExpiration < DateTime.UtcNow) customer.ThrowConflict("OTP is expire!");

                    customer.IsActive = true;
                    customerRepository.Update(customer);
                    await customerRepository.SaveChangesAsync(cancellationToken);

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