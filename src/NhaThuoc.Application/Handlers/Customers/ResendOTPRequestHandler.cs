using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class ResendOTPRequestHandler : IRequestHandler<ResendOTPRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IEmailService emailService;

        public ResendOTPRequestHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            this.customerRepository = customerRepository;
            this.emailService = emailService;
        }

        public async Task<ApiResponse> Handle(ResendOTPRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var customer = await customerRepository.FindSingleAsync(x => x.Email == request.Email && !x.IsActive);
                    if (customer is null) customer.ThrowNotFound();
                    var otp = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
                    customer.OTP = otp;
                    customer.OTPExpiration = DateTime.Now.AddSeconds(90);
                    customerRepository.Update(customer);
                    await customerRepository.SaveChangesAsync(cancellationToken);
                    var subject = "Xác thực tài khoản!";
                    var body = $"<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; color: #333;'>\r\n        <div style='padding: 20px; border-bottom: 3px solid #4CAF50; text-align: center;'>\r\n            <img src='https://drive.google.com/uc?export=view&id=16HsextqqzKJklrRHmX4Qi3RNRM-x1s6e\r\n' alt='Logo' style='width: 150px; margin-bottom: 10px;' />\r\n            <h2 style='color: #4CAF50; font-weight: bold; margin: 0;'>Xác thực tài khoản</h2>\r\n        </div>\r\n        <div style='padding: 20px;'>\r\n            <p>Xin chào,<br>\r\n            Cảm ơn bạn đã đăng ký tài khoản tại hệ thống của chúng tôi. Để hoàn tất quá trình đăng ký, vui lòng sử dụng mã xác thực bên dưới:</p>\r\n            <div style='margin: 20px 0; padding: 15px; background-color: #f8f8f8; border-radius: 8px; text-align: center; font-size: 24px; font-weight: bold; color: #4CAF50;'>\r\n                {otp}\r\n            </div>\r\n            <p>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>\r\n            <p>Trân trọng,<br/>HHT Pharmacy Community!</p>\r\n        </div>\r\n        <div style='background-color: #4CAF50; color: white; padding: 10px; text-align: center; font-size: 12px; border-top: 3px solid #4CAF50;'>\r\n            © 2024 HHT Pharmacy\r\n        </div>\r\n    </div>";
                    await emailService.SendEmailAsync(request.Email, subject, body);
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