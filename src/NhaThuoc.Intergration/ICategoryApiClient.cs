using NhaThuoc.Domain.ReQuest.Category;

namespace NhaThuocOnline.ApiIntergration
{
    public interface ICategoryApiClient
    {
        Task<bool> CreateCategory(CategoryCreateRequest request);
        Task<List<CategoryVm>> GetAllCategories();
    }
}
