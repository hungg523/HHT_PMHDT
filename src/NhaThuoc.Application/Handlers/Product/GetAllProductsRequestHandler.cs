using AutoMapper;
using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<ProductDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductCategoryRepository productCategoryRepository;

        public GetAllProductsRequestHandler(IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productCategoryRepository = productCategoryRepository;
        }

        public Task<List<ProductDTO>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = productRepository.FindAll().ToList();
            var categoryIds = productCategoryRepository.FindAll(x => products.Select(x => x.Id).Contains(x.ProductId)).GroupBy(x => x.ProductId).ToDictionary(g => g.Key, g => g.Select(x => x.CategoryId).ToList());
            var productDtos = mapper.Map<List<ProductDTO>>(products);
            foreach (var productDto in productDtos)
            {
                if (productDto.Id.HasValue && categoryIds.ContainsKey(productDto.Id.Value))
                {
                    var categories = categoryIds[productDto.Id.Value];
                    productDto.CategoryId = categories.FirstOrDefault();
                }
            }
            return Task.FromResult(productDtos);
        }
    }
}