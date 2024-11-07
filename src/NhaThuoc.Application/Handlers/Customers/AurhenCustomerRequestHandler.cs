using MediatR;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class AurhenCustomerRequestHandler : IRequestHandler<AurhenCustomerRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;

        public AurhenCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<ApiResponse> Handle(AurhenCustomerRequest request, CancellationToken cancellationToken)
        {
            
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new AurhenCustomerRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customer = await customerRepository.FindSingleAsync(x => x.Id == request.Id && x.OTP == request.OTP);
                    if (customer is null) customer.ThrowNotFound();

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