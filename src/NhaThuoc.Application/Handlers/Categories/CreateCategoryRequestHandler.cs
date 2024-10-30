using AutoMapper;
using MediatR;
using NhaThuoc.Application.Exceptions;
using NhaThuoc.Application.Extensions;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Validators.Categories;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Categories
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, ApiResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CreateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
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

                    var category = new Category()
                    {
                        Name = request.Name,
                        ParentId = request.ParentId,
                        Description = request.Description,
                        ImagePath = request.ImagePath,
                        IsActive = request.IsActive,
                        CreatedAt = DateTime.UtcNow
                    };
                    
                    categoryRepository.Create(category);
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