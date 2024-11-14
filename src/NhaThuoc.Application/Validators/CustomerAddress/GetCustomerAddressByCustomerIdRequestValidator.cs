using FluentValidation;
using NhaThuoc.Application.Request.Customers.CustomerAddress;

namespace NhaThuoc.Application.Validators.CustomerAddress
{
    public class GetCustomerAddressByCustomerIdRequestValidator : AbstractValidator<GetCustomerAddressByCustomerIdRequest>
    {
        public GetCustomerAddressByCustomerIdRequestValidator()
        {
            RuleFor(o => o.CustomerId)
                .NotNull().WithMessage("CustomerId không được để trống.")
                .GreaterThan(0).WithMessage("CustomerId phải lớn hơn 0.");
        }
    }
}