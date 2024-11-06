using FluentValidation;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull().WithMessage("FirstName không được để trống.")
                .NotEmpty().WithMessage("FirstName không được rỗng.")
                .MaximumLength(200).WithMessage("FirstName không được vượt quá 200 ký tự.");

            RuleFor(u => u.LastName)
                .NotNull().WithMessage("LastName không được để trống.")
                .NotEmpty().WithMessage("LastName không được rỗng.")
                .MaximumLength(200).WithMessage("LastName không được vượt quá 200 ký tự.");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("Email không đúng định dạng.")
                .MaximumLength(450).WithMessage("LastName không được vượt quá 450 ký tự.");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password không được để trống.")
                .NotEmpty().WithMessage("Password không được rỗng.")
                .MaximumLength(2000).WithMessage("Password không vượt quá 2000 ký tự.");

            RuleFor(u => u.ConfirmPassword)
                .NotNull().WithMessage("ConfirmPassword không được để trống.")
                .NotEmpty().WithMessage("ConfirmPassword không được rỗng.")
                .Equal(u => u.Password).WithMessage("ConfirmPassword phải trùng với Password.");
        }
    }
}
