﻿using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Application.Validators.Coupon;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Extensions;

namespace NhaThuoc.Application.Handlers.Coupon
{
    public class CreateCouponRequestHandler : IRequestHandler<CouponCreateRequest, ApiResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        public CreateCouponRequestHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(CouponCreateRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = couponRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateCouponRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var coupon = mapper.Map<Domain.Entities.Coupon>(request);
                    coupon.UpdatedAt = coupon.CreatedAt;

                    couponRepository.Create(coupon);
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