using FluentValidation;
using NhaThuoc.Application.Request.Product;

namespace NhaThuoc.Application.Validators.Product
{
    public class DeleteProductRequestValidator : AbstractValidator<ProductDeleteRequest>
    {
        public DeleteProductRequestValidator() 
        {
            RuleFor(c => c.Id)
             .NotNull().WithMessage("Id không được để trống.")
             .NotEmpty()
             .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }
}
