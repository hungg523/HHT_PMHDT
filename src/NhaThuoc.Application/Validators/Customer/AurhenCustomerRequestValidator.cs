using FluentValidation;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class AurhenCustomerRequestValidator : AbstractValidator<AurhenCustomerRequest>
    {
        public AurhenCustomerRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id không được để trống.")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0");

            RuleFor(x => x.OTP).Length(6).WithMessage("OTP phải đủ 6 ký tự");
        }
    }
}