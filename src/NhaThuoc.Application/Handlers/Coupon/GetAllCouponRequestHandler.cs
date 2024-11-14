using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Coupon
{
    public class GetAllCouponRequestHandler : IRequestHandler<GetAllCouponRequest, List<Entities.Coupon>>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;

        public GetAllCouponRequestHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<List<Entities.Coupon>> Handle(GetAllCouponRequest request, CancellationToken cancellationToken)
        {
            var category = couponRepository.FindAll(x => x.TimesUsed < x.MaxUsage && x.CouponEndDate > DateTime.Now);
            if(category is null) category.ThrowNotFound();
            return mapper.Map<List<Entities.Coupon>>(category);
        }
    }
}
