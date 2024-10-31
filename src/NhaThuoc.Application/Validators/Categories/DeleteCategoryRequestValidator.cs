using FluentValidation;
using NhaThuoc.Application.Request.Category;

namespace NhaThuoc.Application.Validators.Categories
{
    public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
    {
        public DeleteCategoryRequestValidator()
        {
            RuleFor(c => c.Id)
              .NotNull().WithMessage("Id không được để trống.")
              .NotEmpty()
              .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }
}
