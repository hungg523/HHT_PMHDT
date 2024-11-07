using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        public RegisterRequestHandler(ICustomerRepository customerRepository, IMapper mapper, IEmailService emailService)
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
                    var existingCustomer = await customerRepository.FindSingleAsync(x => x.Email == request.Email);
                    if (existingCustomer is not null) existingCustomer.ThrowConflict();

                    var validator = new RegisterRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    var otp = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

                    var customer = new Customer
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Password = hashedPassword,
                        OTP = otp,
                        Role = 0,
                        IsActive = false
                    };

                    customerRepository.Create(customer);
                    await customerRepository.SaveChangesAsync(cancellationToken);
                    var subject = "Xác thực tài khoản của bạn";
                    var body = $"Mã xác thực của bạn là: {otp}";
                    await emailService.SendEmailAsync(request.Email, subject, body);
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