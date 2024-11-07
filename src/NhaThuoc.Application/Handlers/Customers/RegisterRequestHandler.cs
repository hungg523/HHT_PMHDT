using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly EmailService emailService;

        public RegisterRequestHandler(ICustomerRepository customerRepository, IMapper mapper, EmailService emailService)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        public async Task<ApiResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var existingCustomer = await customerRepository.FindAll(u => u.Email == request.Email).FirstOrDefaultAsync(cancellationToken);

                    if (existingCustomer == null)
                    {
                        var validator = new RegisterRequestValidator();
                        var validationResult = await validator.ValidateAsync(request, cancellationToken);
                        validationResult.ThrowIfInvalid();

                        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                        var otp = GenerateVerificationCode();

                        var customer = new Customer
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Email = request.Email,
                            Password = hashedPassword,
                            IsActive = false, 
                            OTP = otp 
                        };

                        customerRepository.Create(customer);
                        await customerRepository.SaveChangesAsync(cancellationToken);

                        var subject = "Xác thực tài khoản của bạn";
                        var body = $"Mã xác thực của bạn là: {otp}";
                        await emailService.SendEmailAsync(request.Email, subject, body);

                        await transaction.CommitAsync(cancellationToken);

                        return new ApiResponse
                        {
                            IsSuccess = true,
                            StatusCode = StatusCodes.Status200OK,
                        };
                    }
                    else
                    {
                        if (request.OTP != existingCustomer.OTP)
                        {
                            return new ApiResponse
                            {
                                IsSuccess = false,
                                StatusCode = StatusCodes.Status400BadRequest,
                            };
                        }

                        existingCustomer.IsActive = true;
                        existingCustomer.OTP = null; 

                        customerRepository.Update(existingCustomer);
                        await customerRepository.SaveChangesAsync(cancellationToken);

                        await transaction.CommitAsync(cancellationToken);

                        return new ApiResponse
                        {
                            IsSuccess = true,
                            StatusCode = StatusCodes.Status200OK,
                        };
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000); 
            return code.ToString();
        }
    }
}
