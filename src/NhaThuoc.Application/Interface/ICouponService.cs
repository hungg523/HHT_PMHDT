using NhaThuoc.Domain.ReQuest.Coupon;

namespace NhaThuoc.Application.Interface
{
    public interface ICouponService
    {
        Task<bool> CreateCouponCode(CouponCreateRequest request);
        Task<List<CouponVm>> GetAllCouponList();
    }
}
