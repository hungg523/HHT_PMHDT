using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Intergration.IApiClient
{
    public interface ICouponApiClient
    {
        Task<bool> CreateCouponCode(CouponCreateRequest request);
        Task<List<Coupon>> GetAllCouponList();
    }
}
