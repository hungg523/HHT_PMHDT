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
        }
    }
}
