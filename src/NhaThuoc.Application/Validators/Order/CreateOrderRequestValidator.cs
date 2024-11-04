using FluentValidation;
using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuoc.Application.Validators.Order
{
    public class CreateOrderRequestValidator : AbstractValidator<OrderCreateRequest>
    {
        public CreateOrderRequestValidator() 
        {
            RuleFor(o => o.CouponId)
                .GreaterThan(0).When(o => o.CouponId > 0).WithMessage("Mã giảm giá phải lớn hơn 0 nếu có.");

            RuleFor(o => o.CustomerId)
                .NotNull().WithMessage("Mã khách hàng không được để trống.")
                .GreaterThan(0).WithMessage("Mã khách hàng phải lớn hơn 0.");

            RuleFor(o => o.CreatedAt)
                .NotNull().WithMessage("Ngày tạo không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày tạo không được lớn hơn ngày hiện tại.");

            RuleFor(o => o.UpdatedAt)
                .NotNull().WithMessage("Ngày cập nhật không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày cập nhật không được lớn hơn ngày hiện tại.");
        }
    }
}
