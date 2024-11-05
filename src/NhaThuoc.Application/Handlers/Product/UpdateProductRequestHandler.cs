using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Application.Validators.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, ApiResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public UpdateProductRequestHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.productCategoryRepository = productCategoryRepository;
        }

        public async Task<ApiResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = categoryRepository.BeginTransaction())
            {
                try
                {
                    var validator = new UpdateProductRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var product = await productRepository.FindByIdAsync(request.Id);
                    if(product is null) product.ThrowNotFound();
                    if (request.CategoryIds is not null && request.CategoryIds.Any()) await categoryRepository.CheckIdsExistAsync(request.CategoryIds.ToList());

                    var dubCategoryId = request.CategoryIds.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                    if (dubCategoryId.Any()) dubCategoryId.ThrowConflict();

                    product.ProductName = request.ProductName ?? product.ProductName;
                    product.RegularPrice = request.RegularPrice ?? product.RegularPrice;
                    product.Description = request.Description ?? product.Description;
                    product.DiscountPrice = request.DiscountPrice ?? product.DiscountPrice;
                    product.Brand = request.Brand ?? product.Brand;
                    product.Packaging = request.Packaging ?? product.Packaging;
                    product.Origin = request.Origin ?? product.Origin;
                    product.Manufacturer = request.Manufacturer ?? product.Manufacturer;
                    product.Ingredients = request.Ingredients ?? product.Ingredients;
                    product.ImagePath = request.ImagePath ?? product.ImagePath;
                    product.SeoAlias = request.SeoAlias ?? product.SeoAlias;
                    product.SeoTitle = request.SeoTitle ?? product.SeoTitle;
                    product.IsActived = request.IsActived ?? product.IsActived;
                    
                    if(request.CategoryIds is not null)
                    {                    
                         var existingProduct = productCategoryRepository.FindAll(x => x.ProductId == request.Id).ToList();
                         productCategoryRepository.RemoveMultiple(existingProduct);
                    }    
                    product.ProductCategories = request.CategoryIds?.Distinct().Select(categoryId => new Entities.ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    }).ToList() ?? product.ProductCategories;
                    productRepository.Update(product);
                    await productRepository.SaveChangesAsync();
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