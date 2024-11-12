using FluentValidation;
using NhaThuoc.Application.Request.Product;

namespace NhaThuoc.Application.Validators.Product
{
    public class GetProductDeatilRequestValidator : AbstractValidator<GetProductDeatilRequest>
    {
        public GetProductDeatilRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id không được null.").NotEmpty().WithMessage("Id không được rỗng.").GreaterThan(0).WithMessage("Id phải lớn hơn 0!");
        }
    }
}