using FluentValidation;
using NhaThuoc.Application.Request.Customers.CustomerAddress;

namespace NhaThuoc.Application.Validators.CustomerAddress
{
    public class DeleteCustomerAddressRequestValidator : AbstractValidator<CustomerAddressDeleteRequest>
    {
        public DeleteCustomerAddressRequestValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }
}
