using FluentValidation;
using NhaThuoc.Application.Request.Customer;

namespace NhaThuoc.Application.Validators.Customer
{
    public class UpdateCustomerRequestValidator : AbstractValidator<CustomerUpdateRequest>
    {
        public UpdateCustomerRequestValidator() 
        {
            RuleFor(c => c.CustomerId)
                .NotNull().WithMessage("Mã khách hàng không được để trống.")
                .GreaterThan(0).WithMessage("Mã khách hàng phải lớn hơn 0.");

            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("Tên không được để trống.")
                .NotEmpty().WithMessage("Tên không được rỗng.")
                .MaximumLength(50).WithMessage("Tên không được vượt quá 50 ký tự.");

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Họ không được để trống.")
                .NotEmpty().WithMessage("Họ không được rỗng.")
                .MaximumLength(50).WithMessage("Họ không được vượt quá 50 ký tự.");

            RuleFor(c => c.PhoneNumber)
                .NotNull().WithMessage("Số điện thoại không được để trống.")
                .NotEmpty().WithMessage("Số điện thoại không được rỗng.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Số điện thoại không hợp lệ.");

            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email không được để trống.")
                .NotEmpty().WithMessage("Email không được rỗng.")
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(c => c.AvatarImagePath)
                .MaximumLength(250).WithMessage("Đường dẫn ảnh đại diện không được vượt quá 250 ký tự.")
                .Matches(@"^(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|png)$")
                .When(c => !string.IsNullOrEmpty(c.AvatarImagePath))
                .WithMessage("Đường dẫn ảnh đại diện không hợp lệ. Chỉ chấp nhận các định dạng jpg, jpeg, png.");
        }
    }
}
