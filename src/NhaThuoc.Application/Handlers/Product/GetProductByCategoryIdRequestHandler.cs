using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetProductByCategoryIdRequestHandler : IRequestHandler<GetProductByCategoryIdRequest, List<Entities.Product>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public GetProductByCategoryIdRequestHandler(IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository = null, ICategoryRepository categoryRepository = null)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productCategoryRepository = productCategoryRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task<List<Entities.Product>> Handle(GetProductByCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindByIdAsync(request.CategoryId);
            if(category is null) category.ThrowNotFound();
            var categoryIds = productCategoryRepository.FindAll(x => x.CategoryId == request.CategoryId).Select(x => x.ProductId).ToList();
            var products = productRepository.FindAll(x => categoryIds.Contains(x.Id)).ToList();
            return mapper.Map<List<Entities.Product>>(products);
        }
    }
}
