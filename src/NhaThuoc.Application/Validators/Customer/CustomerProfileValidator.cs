using FluentValidation;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class CustomerProfileValidator : AbstractValidator<CustomerProfileDto>
    {
        public CustomerProfileValidator() 
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("Id không được để trống.")
                .NotEmpty().WithMessage("Id không được để rỗng.")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(u => u.FirstName)
                .NotNull().WithMessage("FirstName không được để trống.")
                .NotEmpty().WithMessage("FirstName không được rỗng.")
                .MaximumLength(200).WithMessage("FirstName không được vượt quá 200 ký tự.");

            RuleFor(u => u.LastName)
                .NotNull().WithMessage("LastName không được để trống.")
                .NotEmpty().WithMessage("LastName không được rỗng.")
                .MaximumLength(200).WithMessage("LastName không được vượt quá 200 ký tự.");

            RuleFor(c => c.PhoneNumber)
                .NotNull().WithMessage("Phone không được để trống.")
                .NotEmpty().WithMessage("Phone không được rỗng.")
                .MaximumLength(20).WithMessage("Phone không được vượt quá 20 ký tự.");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("Email không đúng định dạng.")
                .MaximumLength(450).WithMessage("LastName không được vượt quá 450 ký tự.");
        }
    }
}
