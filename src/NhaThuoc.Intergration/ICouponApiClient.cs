using NhaThuoc.Domain.ReQuest.Coupon;

namespace NhaThuocOnline.ApiIntergration
{
    public interface ICouponApiClient
    {
        Task<bool> CreateCouponCode(CouponCreateRequest request);
        Task<List<CouponVm>> GetAllCouponList();
    }
}
