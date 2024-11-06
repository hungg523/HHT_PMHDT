using FluentValidation;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(l => l.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("EmailAddress không hợp lệ.");

            RuleFor(l => l.Password)
                .NotNull().WithMessage("Password không được để trống.")
                .NotEmpty().WithMessage("Password không được rỗng.")
                .MaximumLength(2000).WithMessage("Password không vượt quá 2000 ký tự.");
        }
    }
}
