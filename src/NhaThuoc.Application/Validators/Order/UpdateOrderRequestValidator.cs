using FluentValidation;
using NhaThuoc.Application.Request.Order;

namespace NhaThuoc.Application.Validators.Order
{
    public class UpdateOrderRequestValidator : AbstractValidator<OrderUpdateRequest>
    {
        public UpdateOrderRequestValidator() 
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("Id không được để trống.")
                .NotEmpty()
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(o => o.CouponId)
                .GreaterThan(0).WithMessage("CouponId phải lớn hơn 0.");

            RuleFor(o => o.CustomerId)
                .GreaterThan(0).WithMessage("CustomerId phải lớn hơn 0.");
        }
    }
}
