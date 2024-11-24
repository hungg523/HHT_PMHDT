using AutoMapper;
using MediatR;
using NhaThuoc.Application.Validators.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.ReQuest.Order;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Enums;
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
        private readonly IOrderItemRepository orderItemRepository;

        public CreateOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, ICouponRepository couponRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.couponRepository = couponRepository;
            this.productRepository = productRepository;
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

                    var order = new Entities.Order
                    {
                        CustomerId = request.CustomerId,
                        CustomerAddressId = request.CustomerAddressId,
                        Payment = request.PaymentMethod,
                        Status = OrderStatus.Pending,
                        TotalPrice = 0,
                        CreatedAt = DateTime.Now,
                    }; 

                    if (request.CouponCode is not null)
                    {
                        coupon = await couponRepository.FindSingleAsync(x => x.Code == request.CouponCode || !x.IsActive || x.TimesUsed == x.MaxUsage || x.CouponEndDate < DateTime.Now);
                        if (coupon is null) coupon.ThrowNotFound("Mã giảm giá không hợp lệ.");
                        order.CouponId = coupon!.Id;
                    }
                    
                    orderRepository.Create(order);
                    await orderRepository.SaveChangesAsync(cancellationToken);

                    decimal orderTotalPrice = 0;

                    foreach (var item in request.OrderItems)
                    {
                        var product = await productRepository.FindByIdAsync(item.ProductId);
                        if (product is null) product.ThrowNotFound();

                        decimal price = (product.DiscountPrice.HasValue && product.DiscountPrice > 0) ? (decimal)product.DiscountPrice.Value : (decimal)(product.RegularPrice ?? 0);
                        var orderItem = new Entities.OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            TotalPrice = (decimal)(price * item.Quantity),
                        };
                        orderTotalPrice += orderItem.TotalPrice;
                        orderItemRepository.Create(orderItem);
                    }

                    await orderItemRepository.SaveChangesAsync(cancellationToken);
                    if (coupon is not null)
                    {
                        decimal discount = decimal.Parse(coupon.Discount);
                        orderTotalPrice -= discount;
                        if (orderTotalPrice < 0) orderTotalPrice = 0;

                        coupon.TimesUsed += 1;
                        couponRepository.Update(coupon);
                        await couponRepository.SaveChangesAsync(cancellationToken);
                    }

                    order.TotalPrice = orderTotalPrice;
                    orderRepository.Update(order);
                    await orderRepository.SaveChangesAsync(cancellationToken);

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
