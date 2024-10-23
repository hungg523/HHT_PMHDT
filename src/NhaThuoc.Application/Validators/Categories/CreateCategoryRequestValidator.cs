using FluentValidation;
using NhaThuoc.Application.Request.Category;

namespace NhaThuoc.Application.Validators.Categories
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(c => c.ParentId)
                .NotNull().WithMessage("ParentId không được để trống.")
                .NotEmpty()
                .GreaterThan(0).WithMessage("ParentId phải lớn hơn 0.");

            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name không được để trống.")
                .NotEmpty().WithMessage("Name không được rỗng.")
                .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Description không được vượt quá 500 ký tự.");

            RuleFor(c => c.ImagePath)
                .NotNull().WithMessage("ImagePath không được để trống.")
                .NotEmpty().WithMessage("ImagePath không được rỗng.")
                .WithMessage("Đường dẫn hình ảnh không hợp lệ.");

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("Trạng thái hoạt động không được để trống.");

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