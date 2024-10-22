using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Validators.Categories;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.Exceptions;

namespace NhaThuoc.Application.Handlers.Categories
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, NhaThuocException>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CreateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<NhaThuocException> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = categoryRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CreateCategoryRequestValidator();
                    var category = mapper.Map<Category>(request);
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        return new NhaThuocException(false, 400, errors);
                    }

                    if (category is null)
                    {
                        return new NhaThuocException(false, 400);
                    }

                    category.CreatedAt = DateTime.UtcNow;
                    categoryRepository.Create(category);
                    await categoryRepository.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);

                    return new NhaThuocException(true, 200);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new NhaThuocException(false, 500);
                }
            }
        }
    }
}