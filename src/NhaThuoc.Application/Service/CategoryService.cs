using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Interface;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.Data;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.ReQuest.Category;

namespace NhaThuoc.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateCategory(CategoryCreateRequest request)
        {

        var newCategory = new Category()
            {
                ParentId = request.ParentId,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImagePath = "img/default-category.img",
                IsActive = true,
                
            };
          _dbContext.Categories.Add(newCategory);
            await _dbContext.SaveChangesAsync();
         return true;
            
        }

        public async Task<List<CategoryVm>> GetAllCategories()
        {
            var data= await _dbContext.Categories.Select(x=> new CategoryVm
            {
                 Id = x.Id,
                 ParentId = x.ParentId,
                 CategoryName = x.CategoryName,
                 CategoryDescription = x.CategoryDescription,
                 ImagePath= x.ImagePath,
                 IsActive= x.IsActive,
            }).ToListAsync();

            return new List<CategoryVm>(data);
        }
    }
}
