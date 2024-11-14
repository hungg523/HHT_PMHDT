using AutoMapper;
using MediatR;
using NhaThuoc.Application.Validators.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.ReQuest.Order;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Order
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, ApiResponse>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ICouponRepository couponRepository;
        private readonly IProductRepository productRepository;
        private readonly IApplyCouponRepository applyCouponRepository;
        private readonly IOrderItemRepository orderItemRepository;

        public CreateOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, ICouponRepository couponRepository, IProductRepository productRepository, IApplyCouponRepository applyCouponRepository, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.couponRepository = couponRepository;
            this.productRepository = productRepository;
            this.applyCouponRepository = applyCouponRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<ApiResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = orderRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateOrderRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    Entities.Coupon coupon = null;
                    if (request.CouponCode is not null)
                    {
                        coupon = await couponRepository.FindSingleAsync(x => x.Code == request.CouponCode || !x.IsActive || x.TimesUsed == x.MaxUsage || x.CouponEndDate < DateTime.Now);
                        if (coupon is null) coupon.ThrowNotFound("Mã giảm giá không hợp lệ.");
                    }

                    var order = mapper.Map<Entities.Order>(request);
                    order.CouponId = coupon!.Id;
                    orderRepository.Create(order);
                    await orderRepository.SaveChangesAsync(cancellationToken);

                    foreach (var item in request.OrderItems)
                    {
                        var product = await productRepository.FindByIdAsync(item.ProductId);
                        if (product is null) product.ThrowNotFound();

                        decimal price = (product.DiscountPrice.HasValue && product.DiscountPrice > 0) ? (decimal)product.DiscountPrice.Value : (decimal)(product.RegularPrice ?? 0);

                        if (coupon is not null)
                        {
                            var applyCouponEntry = new Entities.ApplyCoupon
                            {
                                CouponId = coupon.Id,
                                ProductId = item.ProductId,
                            };

                            applyCouponRepository.Create(applyCouponEntry);
                            await applyCouponRepository.SaveChangesAsync(cancellationToken);

                            var applyCoupon = await applyCouponRepository.FindSingleAsync(ac => ac.CouponId == coupon.Id && ac.ProductId == item.ProductId);
                            
                            if (applyCoupon is not null)
                            {
                                decimal discount = decimal.Parse(coupon.Discount);
                                price -= discount;
                            }
                        }
                        var orderItem = new Entities.OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            TotalPrice = price * item.Quantity,
                        };
                        orderItemRepository.Create(orderItem);
                    }
                    await orderItemRepository.SaveChangesAsync(cancellationToken);
                    if (coupon is not null)
                    {
                        coupon.TimesUsed += 1;
                        couponRepository.Update(coupon);
                        await couponRepository.SaveChangesAsync(cancellationToken);
                    }

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
