using AssetServer.Enumerations;
using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Application.Validators.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, ApiResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileService;

        public CreateProductRequestHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, IFileService fileService)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        public async Task<ApiResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = categoryRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateProductRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var product = mapper.Map<Entities.Product>(request);
                    if (request.CategoryIds is not null && request.CategoryIds.Any()) await categoryRepository.CheckIdsExistAsync(request.CategoryIds.ToList());
                    
                    var dubCategoryId = request.CategoryIds.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                    if(dubCategoryId.Any()) dubCategoryId.ThrowConflict();

                    if (request.ImageData is not null)
                    {
                        var fileName = $"a{fileService.GetFileExtensionFromBase64(request.ImageData)}";
                        var path = await fileService.UploadFile(fileName, request.ImageData, AssetType.PRODUCT_IMG);
                        product.ImagePath = path;
                    }

                    product.ProductCategories = request.CategoryIds?.Distinct().Select(categoryId => new Entities.ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    }).ToList();
                    productRepository.Create(product);
                    await categoryRepository.SaveChangesAsync();
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