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
                .GreaterThan(0).When(o => o.CouponId > 0).WithMessage("Mã giảm giá phải lớn hơn 0 nếu có.");

            RuleFor(o => o.CustomerId)
                .NotNull().WithMessage("Mã khách hàng không được để trống.")
                .GreaterThan(0).WithMessage("Mã khách hàng phải lớn hơn 0.");

            RuleFor(o => o.UpdatedAt)
                .NotNull().WithMessage("Ngày cập nhật không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày cập nhật không được lớn hơn ngày hiện tại.");
        }
    }
}
