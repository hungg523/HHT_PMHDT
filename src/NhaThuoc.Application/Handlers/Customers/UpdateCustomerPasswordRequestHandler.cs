using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class UpdateCustomerPasswordRequestHandler : IRequestHandler<UpdateCustomerPasswordRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateCustomerPasswordRequestHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPasswordRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new UpdateCustomerPasswordRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customer = await customerRepository.FindSingleAsync(x => x.Email == request.Email && x.OTP == request.OTP);
                    if (customer is null) customer.ThrowConflict("OTP không hợp lệ!");

                    if (customer.OTPExpiration < DateTime.UtcNow) customer.ThrowConflict("OTP đã quá hạn!");

                    customer.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
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