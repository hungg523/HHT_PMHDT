using FluentValidation;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("Email không đúng định dạng.")
                .MaximumLength(50).WithMessage("LastName không được vượt quá 450 ký tự.");
        }
    }
}