using AssetServer.Enumerations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Validators.Categories;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Category
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, ApiResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileService;

        public CreateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper, IFileService fileService)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        public async Task<ApiResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = categoryRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateCategoryRequestValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var category = mapper.Map<Entities.Category>(request);
                    if (request.ImageData is not null)
                    {
                        var fileName = $"a{fileService.GetFileExtensionFromBase64(request.ImageData)}";
                        var path = await fileService.UploadFile(fileName, request.ImageData, AssetType.CAT_IMG);
                        category.ImagePath = path;
                    }
                        
                    categoryRepository.Create(category);
                    await categoryRepository.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return ApiResponse.Success();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status500InternalServerError,
                        StageTrace = e.StackTrace
                    };
                }
            }
        }
    }
}