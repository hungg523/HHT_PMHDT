using NhaThuoc.Domain.ReQuest.Coupon;

namespace NhaThuoc.Intergration.IApiClient
{
    public interface ICouponApiClient
    {
        Task<bool> CreateCouponCode(CouponCreateRequest request);
        Task<List<CouponVm>> GetAllCouponList();
    }
}
