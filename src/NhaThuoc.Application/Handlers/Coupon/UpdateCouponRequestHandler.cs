using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Application.Validators.Coupon;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Coupon
{
    public class UpdateCouponRequestHandler : IRequestHandler<CouponUpdateRequest, ApiResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        public UpdateCouponRequestHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(CouponUpdateRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = couponRepository.BeginTransaction())
            {
                try
                {
                    var validator = new UpdateCouponRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var coupon = await couponRepository.FindByIdAsync(request.Id!);
                    if (coupon is null) coupon.ThrowNotFound();
                    coupon!.Code = request.Code ?? coupon.Code;
                    coupon.Description = request.Description ?? coupon.Description;
                    coupon.TimesUsed = request.TimesUsed ?? coupon.TimesUsed;
                    coupon.MaxUsage = request.MaxUsage ?? coupon.MaxUsage;
                    coupon.Discount = request.Discount ?? coupon.Discount;
                    coupon.IsActive = request.IsActive ?? coupon.IsActive;

                    couponRepository.Update(coupon);
                    await couponRepository.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return ApiResponse.Success();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
