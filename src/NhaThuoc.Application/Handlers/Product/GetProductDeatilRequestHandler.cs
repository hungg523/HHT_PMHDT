using AutoMapper;
using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Application.Validators.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetProductDeatilRequestHandler : IRequestHandler<GetProductDeatilRequest, ProductDTO>
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;

        public GetProductDeatilRequestHandler(IProductRepository productRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductDeatilRequest request, CancellationToken cancellationToken)
        {
            var validator = new GetProductDeatilRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            validationResult.ThrowIfInvalid();

            var product = await productRepository.FindByIdAsync(request.Id);
            var productDto = mapper.Map<ProductDTO>(product);
            if (product is null) product.ThrowNotFound();

            var successfulOrders = orderRepository.FindAll(x => x.Status == OrderStatus.Successed).ToList();
            var successfulOrderIds = successfulOrders.Select(o => o.Id).ToList();
            var orderItems = orderItemRepository.FindAll(x => successfulOrderIds.Contains(x.OrderId)).ToList();
            var productSaleCount = orderItems.GroupBy(item => item.ProductId).ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));
            productDto.AmountSeller = productSaleCount.GetValueOrDefault(productDto.Id, 0);
            return productDto;
        }
    }
}