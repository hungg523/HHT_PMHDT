using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Coupon
{
    public class GetByNameCouponRequestHandler : IRequestHandler<GetByNameCouponRequest, Domain.Entities.Coupon>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;

        public GetByNameCouponRequestHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.Coupon> Handle(GetByNameCouponRequest request, CancellationToken cancellationToken)
        {
            var category = await couponRepository.FindByIdAsync(request.Id);
            return mapper.Map<Domain.Entities.Coupon>(category);
        }
    }
}
