using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Extensions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public RegisterRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new RegisterRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var existingCustomer = await customerRepository.FindAll(u => u.Email == request.Email).FirstOrDefaultAsync(cancellationToken);
                    if (existingCustomer != null)
                    {
                        return new ApiResponse
                        {
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status400BadRequest,
                        };
                    }

                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    var customer = new Customer
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Password = hashedPassword,
                        IsActive = true,
                       // CreatedAt = DateTime.UtcNow
                    };

                    customerRepository.Create(customer);
                    await customerRepository.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return ApiResponse.Success();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
