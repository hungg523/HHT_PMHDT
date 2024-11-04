using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.Exceptions;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace NhaThuoc.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
            this.context = context;
        }

        public async Task<(List<int?> existingIds, List<int?> missingIds)> CheckIdsExistAsync(List<int?> ids)
        {
            ids = ids.Distinct().ToList();
            var existingIds = await context.Set<Category>()
                                           .Where(category => ids.Contains(category.Id))
                                           .Select(product => product.Id)
                                           .ToListAsync();
            var missingIds = ids.Except(existingIds).ToList();
            if (missingIds.Any())
            {
                throw new NhaThuocException(StatusCodes.Status404NotFound, new List<string> { $"Id {string.Join(", ", missingIds)} is not found in Category" });
            }
            return (existingIds, missingIds);
        }
    }
}