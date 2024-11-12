using FluentValidation;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class AurhenCustomerRequestValidator : AbstractValidator<AuthenCustomerRequest>
    {
        public AurhenCustomerRequestValidator()
        {
            RuleFor(x => x.OTP).Length(6).WithMessage("OTP phải đủ 6 ký tự");
        }
    }
}