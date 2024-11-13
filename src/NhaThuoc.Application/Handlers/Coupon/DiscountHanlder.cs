using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Coupon
{
    public class DiscountService
    {
        public string ApplyDiscount(Entities.Product product, Entities.Coupon coupon)
        {
            if (coupon == null || !coupon.IsActive)
            {
                return "Mã giảm giá không hợp lệ.";
            }

            if (coupon.TimesUsed >= coupon.MaxUsage)
            {
                return "Mã giảm giá đã hết lượt sử dụng.";
            }

            if (coupon.CouponStartDate > DateTime.UtcNow || coupon.CouponEndDate < DateTime.UtcNow)
            {
                return "Mã giảm giá đã hết hạn hoặc chưa có hiệu lực.";
            }

            coupon.TimesUsed++;

            // Giảm giá
            var discountPercentage = Convert.ToDouble(coupon.Discount.TrimEnd('%')) / 100;
            product.DiscountPrice = product.RegularPrice * (1 - discountPercentage);

            return $"Giảm giá {coupon.Discount} đã được áp dụng.";
        }
    }
}
