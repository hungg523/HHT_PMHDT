using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Application.Validators.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Order
{
    public class ChangeStatusOrderRequestHandler : IRequestHandler<ChangeStatusOrderRequest, ApiResponse>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public ChangeStatusOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(ChangeStatusOrderRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = orderRepository.BeginTransaction())
            {
                try
                {
                    var validator = new ChangeStatusOrderRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var order = await orderRepository.FindByIdAsync(request.Id!);
                    if (order is null) order.ThrowNotFound();

                    order!.Status = request.Status ?? order.Status;

                    orderRepository.Update(order);
                    await orderRepository.SaveChangesAsync();
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
