using FluentValidation;
using NhaThuoc.Domain.ReQuest.Product;

namespace NhaThuoc.Application.Validators.Product
{
    public class CreateProductRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public CreateProductRequestValidator() 
        {
            RuleFor(p => p.ProductName)
                .NotNull().WithMessage("Tên sản phẩm không được để trống.")
                .NotEmpty().WithMessage("Tên sản phẩm không được rỗng.")
                .MaximumLength(100).WithMessage("Tên sản phẩm không được vượt quá 100 ký tự.");

            RuleFor(p => p.SKU)
                .NotNull().WithMessage("Mã SKU không được để trống.")
                .NotEmpty().WithMessage("Mã SKU không được rỗng.")
                .MaximumLength(50).WithMessage("Mã SKU không được vượt quá 50 ký tự.");

            RuleFor(p => p.RegularPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Giá bán phải lớn hơn hoặc bằng 0.");

            RuleFor(p => p.DiscountPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Giá khuyến mãi phải lớn hơn hoặc bằng 0.")
                .LessThanOrEqualTo(p => p.RegularPrice).WithMessage("Giá khuyến mãi không được lớn hơn giá bán.");

            RuleFor(p => p.Description);

            RuleFor(p => p.Brand)
                .MaximumLength(100).WithMessage("Thương hiệu không được vượt quá 100 ký tự.");

            RuleFor(p => p.Packaging)
                .MaximumLength(100).WithMessage("Quy cách đóng gói không được vượt quá 100 ký tự.");

            RuleFor(p => p.Origin)
                .MaximumLength(100).WithMessage("Xuất xứ không được vượt quá 100 ký tự.");

            RuleFor(p => p.Manufacturer)
                .MaximumLength(100).WithMessage("Nhà sản xuất không được vượt quá 100 ký tự.");

            RuleFor(p => p.Ingredients)
                .MaximumLength(1000).WithMessage("Thành phần không được vượt quá 1000 ký tự.");

            RuleFor(p => p.ImagePath)
                .Matches(@"^(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|png)$")
                .When(p => !string.IsNullOrEmpty(p.ImagePath))
                .WithMessage("Đường dẫn hình ảnh không hợp lệ. Chỉ chấp nhận các định dạng jpg, jpeg, png.");

            RuleFor(p => p.SeoTitle)
                .MaximumLength(100).WithMessage("Tiêu đề SEO không được vượt quá 100 ký tự.");

            RuleFor(p => p.SeoAlias)
                .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .When(p => !string.IsNullOrEmpty(p.SeoAlias))
                .WithMessage("Định dạng đường dẫn SEO không hợp lệ.");

            RuleFor(p => p.IsActived)
                .NotNull().WithMessage("Trạng thái kích hoạt không được để trống.");

            RuleFor(p => p.CreatedAt)
                .NotNull().WithMessage("Ngày tạo không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày tạo không được lớn hơn ngày hiện tại.");

            RuleFor(p => p.UpdatedAt)
                .NotNull().WithMessage("Ngày cập nhật không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày cập nhật không được lớn hơn ngày hiện tại.");
        }
    }
}
