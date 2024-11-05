using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Category
{
    public class GetByNameCategoryRequestHandler : IRequestHandler<GetByNameCategoryRequest, Domain.Entities.Category>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public GetByNameCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.Category> Handle(GetByNameCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindByIdAsync(request.Id);
            return mapper.Map<Domain.Entities.Category>(category);
        }
    }
}
