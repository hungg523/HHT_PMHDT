using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;

namespace NhaThuoc.Application.Handlers.Coupon
{
    public class GetByIdCouponRequestHandler : IRequestHandler<GetByIdCouponRequest, Domain.Entities.Coupon>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;

        public GetByIdCouponRequestHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.Coupon> Handle(GetByIdCouponRequest request, CancellationToken cancellationToken)
        {
            var coupon = await couponRepository.FindByIdAsync(request.Id);
            if (coupon is null) coupon.ThrowNotFound();
            return mapper.Map<Domain.Entities.Coupon>(coupon);
        }
    }
}
