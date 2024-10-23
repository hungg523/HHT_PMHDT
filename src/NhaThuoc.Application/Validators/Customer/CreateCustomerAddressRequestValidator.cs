using FluentValidation;
using NhaThuoc.Application.Request.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class CreateCustomerAddressRequestValidator : AbstractValidator<CustomerAddressCreateRequest>
    {
        public CreateCustomerAddressRequestValidator() 
        {
            RuleFor(c => c.CustomerId)
                .NotNull().WithMessage("Mã khách hàng không được để trống.")
                .GreaterThan(0).WithMessage("Mã khách hàng phải lớn hơn 0.");

            RuleFor(c => c.AddressLine1)
                .NotNull().WithMessage("Địa chỉ không được để trống.")
                .NotEmpty().WithMessage("Địa chỉ không được rỗng.")
                .MaximumLength(250).WithMessage("Địa chỉ không được vượt quá 250 ký tự.");

            RuleFor(c => c.AddressLine2)
                .MaximumLength(250).WithMessage("Địa chỉ dòng 2 không được vượt quá 250 ký tự.");

            RuleFor(c => c.ReceiverName)
                .NotNull().WithMessage("Tên người nhận không được để trống.")
                .NotEmpty().WithMessage("Tên người nhận không được rỗng.")
                .MaximumLength(100).WithMessage("Tên người nhận không được vượt quá 100 ký tự.");

            RuleFor(c => c.ReceiverPhone)
                .NotNull().WithMessage("Số điện thoại người nhận không được để trống.")
                .NotEmpty().WithMessage("Số điện thoại người nhận không được rỗng.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Số điện thoại không hợp lệ.");

            RuleFor(c => c.City)
                .NotNull().WithMessage("Thành phố không được để trống.")
                .NotEmpty().WithMessage("Thành phố không được rỗng.")
                .MaximumLength(100).WithMessage("Thành phố không được vượt quá 100 ký tự.");

            RuleFor(c => c.District)
                .NotNull().WithMessage("Quận/Huyện không được để trống.")
                .NotEmpty().WithMessage("Quận/Huyện không được rỗng.")
                .MaximumLength(100).WithMessage("Quận/Huyện không được vượt quá 100 ký tự.");
        }
    }
}
