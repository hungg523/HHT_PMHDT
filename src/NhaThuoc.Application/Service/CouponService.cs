using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.Data;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.ReQuest.Coupon;

namespace NhaThuoc.Application.Service
{
    public class CouponService: ICouponService
    {
        private readonly ApplicationDbContext _dbContext;
        public CouponService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateCouponCode(CouponCreateRequest request)
        {

            var newCoupon = new Coupon()
            {
                Code = request.Code,
                Description = request.Description,
                TimesUsed = 0,
                MaxUsage = request.MaxUsage,
                Discount = request.Discount,
                CouponStartDate = request.CouponStartDate,
                CouponEndDate = request.CouponEndDate,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };
            _dbContext.Coupons.Add(newCoupon);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<CouponVm>> GetAllCouponList()
        {
            var data = await _dbContext.Coupons.Select(x => new CouponVm
            {
               Id = x.Id,
               Code = x.Code,
               TimesUsed = x.TimesUsed,
               MaxUsage = x.MaxUsage,
               DiscountType= x.DiscountType,
               DiscountValue= x.DiscountValue,
               IsActive= x.IsActive,
                CouponDescription = x.CouponDescription,
                CouponStartDate= x.CouponStartDate,
                CouponEndDate = x.CouponEndDate
                
            }).ToListAsync();

            return new List<CouponVm>(data);
        }
    }
}
