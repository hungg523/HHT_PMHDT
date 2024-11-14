using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;

namespace NhaThuoc.Application.Handlers.Order
{
    public class GetByIdOrderRequestHandler : IRequestHandler<GetByIdOrderRequest, Domain.Entities.Order>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetByIdOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.Order> Handle(GetByIdOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.FindByIdAsync(request.Id);
            if (order is null) order.ThrowNotFound();
            return mapper.Map<Domain.Entities.Order>(order);
        }
    }
}
