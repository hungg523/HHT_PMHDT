using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Validators.Categories;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Extensions;

namespace NhaThuoc.Application.Handlers.Category
{
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, ApiResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public UpdateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
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
                    category.ImagePath = request.ImagePath ?? category.ImagePath;
                    category.IsActive = request.IsActive ?? category.IsActive;
                    
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