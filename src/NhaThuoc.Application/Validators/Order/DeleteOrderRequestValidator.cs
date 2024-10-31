using FluentValidation;
using NhaThuoc.Application.Request.Order;

namespace NhaThuoc.Application.Validators.Order
{
    public class DeleteOrderRequestValidator : AbstractValidator<OrderDeleteRequest>
    {
        public DeleteOrderRequestValidator() 
        {
            RuleFor(c => c.Id)
              .NotNull().WithMessage("Id không được để trống.")
              .NotEmpty()
              .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }
}
