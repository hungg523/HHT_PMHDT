﻿using AutoMapper;
using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Enums;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetProductByCategoryIdRequestHandler : IRequestHandler<GetProductByCategoryIdRequest, PagedResponse<ProductDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;

        public GetProductByCategoryIdRequestHandler(IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository = null, ICategoryRepository categoryRepository = null, IOrderRepository orderRepository = null, IOrderItemRepository orderItemRepository = null)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productCategoryRepository = productCategoryRepository;
            this.categoryRepository = categoryRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }
        public async Task<PagedResponse<ProductDTO>> Handle(GetProductByCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindByIdAsync(request.CategoryId);
            if(category is null) category.ThrowNotFound();
            var categoryIds = productCategoryRepository.FindAll(x => x.CategoryId == request.CategoryId).Select(x => x.ProductId).ToList();
            var products = productRepository.FindAll(x => categoryIds.Contains(x.Id)).ToList();
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
