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
                .NotEmpty().WithMessage("ParentId không được để rỗng.")
                .GreaterThan(0).WithMessage("ParentId phải lớn hơn 0.");

            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name không được để trống.")
                .NotEmpty().WithMessage("Name không được rỗng.")
                .MaximumLength(2000).WithMessage("Name không được vượt quá 2000 ký tự.");

            RuleFor(c => c.Description)
                .NotNull().WithMessage("Description không được để trống.")
                .NotEmpty().WithMessage("Description không được rỗng.")
                .MaximumLength(2000).WithMessage("Description không được vượt quá 2000 ký tự.");

            RuleFor(c => c.IsActive)
                .NotEmpty().WithMessage("IsActive không được rỗng.")
                .NotNull().WithMessage("IsActive không được để trống.");
        }
    }
}