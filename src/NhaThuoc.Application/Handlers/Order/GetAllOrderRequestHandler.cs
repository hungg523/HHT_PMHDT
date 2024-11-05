using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Order
{
    public class GetAllOrderRequestHandler : IRequestHandler<GetAllOrderRequest, List<Entities.Order>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetAllOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<List<Entities.Order>> Handle(GetAllOrderRequest request, CancellationToken cancellationToken)
        {
            var category = orderRepository.FindAll();

            return mapper.Map<List<Entities.Order>>(category);
        }
    }
}
