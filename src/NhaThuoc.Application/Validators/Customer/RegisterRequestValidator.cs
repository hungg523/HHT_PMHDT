using FluentValidation;
using NhaThuoc.Application.Request.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.FirstName)
                .NotNull().WithMessage("Tên không được để trống.")
                .NotEmpty().WithMessage("Tên không được rỗng.")
                .MaximumLength(50).WithMessage("Tên không được vượt quá 50 ký tự.");

            RuleFor(r => r.LastName)
                .NotNull().WithMessage("Họ không được để trống.")
                .NotEmpty().WithMessage("Họ không được rỗng.")
                .MaximumLength(50).WithMessage("Họ không được vượt quá 50 ký tự.");

            RuleFor(r => r.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");

            RuleFor(r => r.Password)
                .NotNull().WithMessage("Mật khẩu không được để trống.")
                .NotEmpty().WithMessage("Mật khẩu không được rỗng.")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");

            RuleFor(r => r.ConfirmPassword)
                .NotNull().WithMessage("Xác nhận mật khẩu không được để trống.")
                .NotEmpty().WithMessage("Xác nhận mật khẩu không được rỗng.")
                .Equal(r => r.Password).WithMessage("Mật khẩu xác nhận không khớp với mật khẩu.");
        }
    }
}
