using AutoMapper;
using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;
using System.Linq;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetAllProductRequestHandler : IRequestHandler<GetAllProductRequest, PagedResponse<ProductDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;

        public GetAllProductRequestHandler(IProductRepository productRepository, IMapper mapper, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }
        public async Task<PagedResponse<ProductDTO>> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
        {
            var products = productRepository.FindAll(x => x.IsActived == true).ToList();
            var totalCount = products.Count();
            var pagedProducts = products.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            var productDtos = mapper.Map<List<ProductDTO>>(products);

            var successfulOrders = orderRepository.FindAll(x => x.Status == OrderStatus.Successed).ToList();
            var successfulOrderIds = successfulOrders.Select(o => o.Id).ToList();
            var orderItems = orderItemRepository.FindAll(x => successfulOrderIds.Contains(x.OrderId)).ToList();
            var productSaleCount = orderItems.GroupBy(item => item.ProductId).ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

            foreach (var productDto in productDtos) productDto.AmountSeller = productSaleCount.GetValueOrDefault(productDto.Id, 0);

            return new PagedResponse<ProductDTO>(productDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}