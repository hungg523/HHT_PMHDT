using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public LoginRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new LoginRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customer = await customerRepository.FindAll(u => u.Email == request.Email).FirstOrDefaultAsync(cancellationToken);
                    if (customer is null) customer.ThrowNotFound();

                    if (!customer.IsActive)
                    {
                        return new ApiResponse
                        {
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status403Forbidden,
                        };
                    }

                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, customer.Password);
                    if (!isPasswordValid)
                    {
                        return new ApiResponse
                        {
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status401Unauthorized,
                        };
                    }

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
