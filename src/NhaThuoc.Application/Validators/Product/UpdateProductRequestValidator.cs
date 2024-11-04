using FluentValidation;
using NhaThuoc.Application.Request.Product;

namespace NhaThuoc.Application.Validators.Product
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator() 
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(p => p.ProductName)
               .MaximumLength(200).WithMessage("ProductName không được vượt quá 200 ký tự.");

            RuleFor(p => p.RegularPrice)
                .GreaterThanOrEqualTo(0).WithMessage("RegularPrice phải lớn hơn hoặc bằng 0.");

            RuleFor(p => p.DiscountPrice)
                .GreaterThanOrEqualTo(0).WithMessage("DiscountPrice phải lớn hơn hoặc bằng 0.");

            RuleFor(p => p.Description)
                .MaximumLength(200).WithMessage("Description không được vượt quá 200 ký tự.");

            RuleFor(p => p.Brand)
                .MaximumLength(2000).WithMessage("Brand không được vượt quá 2000 ký tự.");

            RuleFor(p => p.Packaging)
                .MaximumLength(2000).WithMessage("Packaging không được vượt quá 2000 ký tự.");

            RuleFor(p => p.Origin)
                .MaximumLength(2000).WithMessage("Origin không được vượt quá 2000 ký tự.");

            RuleFor(p => p.Manufacturer)
                .MaximumLength(2000).WithMessage("Manufacturer không được vượt quá 2000 ký tự.");

            RuleFor(p => p.Ingredients)
                .MaximumLength(2000).WithMessage("Ingredients không được vượt quá 2000 ký tự.");

            RuleFor(p => p.SeoTitle)
                .MaximumLength(200).WithMessage("SeoTitle không được vượt quá 200 ký tự.");

            RuleFor(p => p.SeoAlias)
                .MaximumLength(200).WithMessage("SeoAlias không được vượt quá 200 ký tự.");

            RuleFor(x => x.CategoryIds).Must(list => list.All(id => id > 0));
        }
    }
}
