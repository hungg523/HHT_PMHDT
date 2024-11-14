using FluentValidation;
using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuoc.Application.Validators.Order
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator() 
        {
            RuleFor(o => o.CustomerId)
                .NotNull().WithMessage("CustomerId không được để trống.")
                .GreaterThan(0).WithMessage("CustomerId phải lớn hơn 0.");

            RuleFor(o => o.CustomerAddressId)
                .NotNull().WithMessage("CustomerAddressId không được để trống.")
                .GreaterThan(0).WithMessage("CustomerAddressId phải lớn hơn 0.");
        }
    }
}
