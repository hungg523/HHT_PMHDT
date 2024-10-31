using FluentValidation;
using NhaThuoc.Application.Request.Coupon;

namespace NhaThuoc.Application.Validators.Coupon
{
    public class DeleteCouponRequestValidator : AbstractValidator<CouponDeleteRequest>
    {
        public DeleteCouponRequestValidator() 
        {
            RuleFor(c => c.Id)
              .NotNull().WithMessage("Id không được để trống.")
              .NotEmpty()
              .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }

    }
}
