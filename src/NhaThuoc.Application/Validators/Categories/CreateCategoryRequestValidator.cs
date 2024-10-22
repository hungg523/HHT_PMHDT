using FluentValidation;
using NhaThuoc.Application.Request.Category;

namespace NhaThuoc.Application.Validators.Categories
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(c => c.ParentId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}