using FluentValidation;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Customers.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class CustomerProfileValidator : AbstractValidator<UpdateProifleCustomerRequest>
    {
        public CustomerProfileValidator() 
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("Id không được để trống.")
                .NotEmpty().WithMessage("Id không được để rỗng.")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(u => u.FirstName)
                .MaximumLength(200).WithMessage("FirstName không được vượt quá 200 ký tự.");

            RuleFor(u => u.LastName)
                .MaximumLength(200).WithMessage("LastName không được vượt quá 200 ký tự.");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(20).WithMessage("PhoneNumber không được vượt quá 20 ký tự.");
        }
    }
}
