using AutoMapper;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
        }
    }
}