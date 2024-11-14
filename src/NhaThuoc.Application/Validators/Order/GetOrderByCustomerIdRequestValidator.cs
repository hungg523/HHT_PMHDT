using FluentValidation;
using NhaThuoc.Application.Request.Order;

namespace NhaThuoc.Application.Validators.Order
{
    public class GetOrderByCustomerIdRequestValidator : AbstractValidator<GetOrderByCustomerIdRequest>
    {
        public GetOrderByCustomerIdRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("CustomerId không được để trống.")
                .GreaterThan(0).WithMessage("CustomerId phải lớn hơn 0.");
        }
    }
}