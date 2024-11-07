using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Domain.Abtractions.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<(List<int>? existingIds, List<int>? missingIds)> CheckIdsExistAsync(List<int>? ids);
    }
}