using FluentValidation;
using NhaThuoc.Application.Request.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class DeleteCustomerRequestValidator : AbstractValidator<CustomerDeleteRequest>
    {
        public DeleteCustomerRequestValidator() 
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("Id không được để trống.")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }
}
