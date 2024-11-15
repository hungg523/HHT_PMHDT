using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;

namespace NhaThuoc.Application.Handlers.Order
{
    public class GetOrderItemByOrderIdRequestHandler : IRequestHandler<GetOrderItemByOrderIdRequest, List<OrderItem>>
    {
        private readonly IOrderItemRepository orderitemRepository;
        private readonly IMapper mapper;

        public GetOrderItemByOrderIdRequestHandler(IOrderItemRepository orderitemRepository, IMapper mapper)
        {
            this.orderitemRepository = orderitemRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrderItem>> Handle(GetOrderItemByOrderIdRequest request, CancellationToken cancellationToken)
        {
            var product = orderitemRepository.FindAll(x => x.OrderId == request.OrderId).ToList();
            if (product is null) product.ThrowNotFound();
            return mapper.Map<List<OrderItem>>(product);
        }
    }
}
