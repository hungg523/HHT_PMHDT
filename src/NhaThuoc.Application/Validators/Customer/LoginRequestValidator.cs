using FluentValidation;
using NhaThuoc.Application.Request.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(l => l.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");

            RuleFor(l => l.Password)
                .NotNull().WithMessage("Mật khẩu không được để trống.")
                .NotEmpty().WithMessage("Mật khẩu không được rỗng.")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");
        }
    }
}
