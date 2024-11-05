using AutoMapper;
using MediatR;
using NhaThuoc.Application.Validators.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.ReQuest.Order;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Extensions;

namespace NhaThuoc.Application.Handlers.Order
{
    public class CreateOrderRequestHandler : IRequestHandler<OrderCreateRequest, ApiResponse>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public CreateOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = orderRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateOrderRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var order = mapper.Map<Domain.Entities.Order>(request);

                    orderRepository.Create(order);
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
