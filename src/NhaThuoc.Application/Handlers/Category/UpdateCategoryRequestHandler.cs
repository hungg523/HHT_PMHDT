using AssetServer.Enumerations;
using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Validators.Categories;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;

namespace NhaThuoc.Application.Handlers.Category
{
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, ApiResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileService;

        public UpdateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper, IFileService fileService)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        public async Task<ApiResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = categoryRepository.BeginTransaction())
            {
                try
                {
                    var validator = new UpdateCategoryRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var category = await categoryRepository.FindByIdAsync(request.Id!);
                    if (category is null) category.ThrowNotFound();
                    category!.Name = request.Name ?? category.Name;
                    category.Description = request.Description ?? category.Description;
                    category.ParentId = request.ParentId ?? category.ParentId;
                    category.IsActive = request.IsActive ?? category.IsActive;
                    if (request.ImageData is not null)
                    {
                        string fileName = (Path.GetFileName(category.ImagePath) is { } name &&
                                Path.GetExtension(name)?.ToLowerInvariant() == Path.GetExtension(request.ImageFileName)?.ToLowerInvariant()) ? name : request.ImageFileName;
                        category.ImagePath = await fileService.UploadFile(fileName, request.ImageData, AssetType.CAT_IMG);
                    }
                    categoryRepository.Update(category);
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