using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Category
{
    public class GetAllCategoryRequestHandler : IRequestHandler<GetAllCategoryRequest, List<Entities.Category>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public GetAllCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<List<Entities.Category>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = categoryRepository.FindAll();
            return mapper.Map<List<Entities.Category>>(category);
        }
    }
}