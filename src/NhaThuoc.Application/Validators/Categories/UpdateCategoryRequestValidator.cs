using FluentValidation;
using NhaThuoc.Application.Request.Category;

namespace NhaThuoc.Application.Validators.Categories
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator() 
        {
            RuleFor(c => c.Id)
               .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(c => c.ParentId)
                .GreaterThan(0).WithMessage("ParentId phải lớn hơn 0.");

            RuleFor(c => c.Name)
                .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Description không được vượt quá 500 ký tự.");
        }

    }
}
