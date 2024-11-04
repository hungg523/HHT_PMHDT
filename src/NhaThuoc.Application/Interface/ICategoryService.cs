using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.ReQuest.Category;

namespace NhaThuoc.Application.Interface
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryCreateRequest request);
        Task<List<CategoryVm>> GetAllCategories();

    }
}
