using AutoMapper;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
