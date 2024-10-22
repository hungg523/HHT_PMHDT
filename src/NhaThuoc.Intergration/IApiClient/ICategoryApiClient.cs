using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Intergration.IApiClient
{
    public interface ICategoryApiClient
    {
        Task<bool> CreateCategory(CreateCategoryRequest request);
        Task<List<Category>> GetAllCategories();
    }
}
