using FluentValidation;
using NhaThuoc.Application.Request.Coupon;

namespace NhaThuoc.Application.Validators.Coupon
{
    public class CreateCouponRequestValidator : AbstractValidator<CouponCreateRequest>
    {
        public CreateCouponRequestValidator()
        {
            RuleFor(c => c.Code)
                .NotNull().WithMessage("Mã coupon không được để trống.")
                .NotEmpty().WithMessage("Mã coupon không được rỗng.")
                .MaximumLength(50).WithMessage("Mã coupon không được vượt quá 50 ký tự.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");

            RuleFor(c => c.TimesUsed)
                .GreaterThanOrEqualTo(0).WithMessage("Số lần sử dụng phải lớn hơn hoặc bằng 0.");

            RuleFor(c => c.MaxUsage)
                .GreaterThanOrEqualTo(0).WithMessage("Số lần sử dụng tối đa phải lớn hơn hoặc bằng 0.")
                .GreaterThanOrEqualTo(c => c.TimesUsed).WithMessage("Số lần sử dụng tối đa phải lớn hơn hoặc bằng số lần đã sử dụng.");

            RuleFor(c => c.Discount)
                .GreaterThan(0).WithMessage("Giảm giá phải lớn hơn 0.")
                .LessThanOrEqualTo(100).WithMessage("Giảm giá không được vượt quá 100%.");

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("Trạng thái hoạt động không được để trống.");

            RuleFor(c => c.CouponStartDate)
                .NotNull().WithMessage("Ngày bắt đầu coupon không được để trống.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Ngày bắt đầu phải là ngày hiện tại hoặc trong tương lai.");

            RuleFor(c => c.CouponEndDate)
                .NotNull().WithMessage("Ngày kết thúc coupon không được để trống.")
                .GreaterThanOrEqualTo(c => c.CouponStartDate).WithMessage("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");

            RuleFor(c => c.CreatedAt)
                .NotNull().WithMessage("Ngày tạo không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày tạo phải là ngày hiện tại hoặc trong quá khứ.");

            RuleFor(c => c.UpdatedAt)
                .NotNull().WithMessage("Ngày cập nhật không được để trống.")
                .GreaterThanOrEqualTo(c => c.CreatedAt).WithMessage("Ngày cập nhật phải lớn hơn hoặc bằng ngày tạo.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày cập nhật phải là ngày hiện tại hoặc trong quá khứ.");
        }
    }
}
