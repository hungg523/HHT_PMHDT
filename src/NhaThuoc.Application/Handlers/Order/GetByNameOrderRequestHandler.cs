using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Order
{
    public class GetByNameOrderRequestHandler : IRequestHandler<GetByNameOrderRequest, Domain.Entities.Order>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetByNameOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.Order> Handle(GetByNameOrderRequest request, CancellationToken cancellationToken)
        {
            var category = await orderRepository.FindByIdAsync(request.Id);
            return mapper.Map<Domain.Entities.Order>(category);
        }
    }
}
