using AutoMapper;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class ApplyCouponRepository : GenericRepository<ApplyCoupon>, IApplyCouponRepository
    {
        public ApplyCouponRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}