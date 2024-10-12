using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.Data;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.ReQuest.Product;
using System.Net.Http.Headers;

namespace NhaThuoc.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly IFileStorageService _storageService;
        public ProductService(ApplicationDbContext dbContext, IFileStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var imagePath = "img/default-product";
            if(request.ImagePath!= null)
            {

                imagePath = await this.SaveFile(request.ImagePath);
            }

            var product = new Product()
            {
                ProductName = request.ProductName,
                SKU = request.SKU,
                RegularPrice = request.RegularPrice,
                DiscountPrice = request.DiscountPrice,
                Description = request.Description,
                Brand = request.Brand,
                Packaging = request.Packaging,
                Origin = request.Origin,
                Manufacturer = request.Manufacturer,
                Ingredients = request.Ingredients,
                SeoTitle = request.SeoTitle,
                SeoAlias = "product",
                IsActived = false, 
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImagePath = imagePath,
            };

            _dbContext.Products.Add(product);
             await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<ProductVm> GetProductById(int id)
        {
             var query = (from p in _dbContext.Products
                         join pic in _dbContext.ProductInCategories on p.Id equals pic.Id into picGroup
                         from pic in picGroup.DefaultIfEmpty()
                         join c in _dbContext.Categories on pic.CategoryId equals c.Id into cGroup
                         from c in cGroup.DefaultIfEmpty()
                         select new { p, pic, c }).AsQueryable();

            if (id > 0)
            {
            var product = await query.FirstOrDefaultAsync(x => x.p.Id == id);
            if (product != null)
            {
                var productVm = new ProductVm()
                {
                    Id = product.p.Id,
                    ProductName = product.p.ProductName,
                    RegularPrice = product.p.RegularPrice,
                    DiscountPrice = product.p.DiscountPrice,
                    Brand = product.p.Brand,
                    CategoryName = product.c.CategoryName,
                    Description = product.p.Description,
                    Packaging = product.p.Packaging,
                    Origin = product.p.Origin,
                    Manufacturer = product.p.Manufacturer,
                    ProductionLocation = product.p.ProductionLocation,
                    Ingredients = product.p.Ingredients,

                    ImagePath = product.p.ImagePath,
                    SeoTitle = product.p.SeoTitle,
                    SeoAlias = product.p.SeoAlias,
                    IsPrescriptionRequired = product.p.isPrescriptionRequired,

                };
                return productVm;
            }

            }
            return new ProductVm();
        }

        public async Task<PagedResult<ProductBasicVm>> GetAllByCategoryIdPaging(GetPublicProductPagingRequest request)
        {
            // join
            var query = (from p in _dbContext.Products
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.Id
                       join c in _dbContext.Categories on pic.CategoryId equals c.Id 
                       select new {p, pic, c}).AsQueryable();

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.ProductName.Contains(request.Keyword));
            }

            if(!string.IsNullOrEmpty(request.OrderBy))
            {
                switch(request.OrderBy)
                {
                    case "nameAsc": query=query.OrderBy(x=>x.p.ProductName); break;
                    case "latestProduct": query=query.OrderByDescending(x=>x.p.CreatedAt); break;
                    case "priceAsc": query = query.OrderBy(x => x.p.DiscountPrice); break;
                    case "priceDesc": query = query.OrderByDescending(x => x.p.DiscountPrice); break;

                }
            }

            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                  .Select(x => new ProductBasicVm()
                  {
                      Id= x.p.Id,
                      ProductName = x.p.ProductName,
                      ImagePath = x.p.ImagePath,
                      DiscountPrice = x.p.DiscountPrice,
                      RegularPrice = x.p.RegularPrice,
                      isPrescriptionRequired = x.p.isPrescriptionRequired
                  }).ToListAsync();
            
            //select

            var pagedResult = new PagedResult<ProductBasicVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;

        }

        public async Task<PagedResult<ProductVm>> GetAllProductsPaging(GetManageProductPagingRequest request)
        {
            // join
            var query = (from p in _dbContext.Products
                         join pic in _dbContext.ProductInCategories on p.Id equals pic.Id into picGroup
                         from pic in picGroup.DefaultIfEmpty()
                         join c in _dbContext.Categories on pic.CategoryId equals c.Id into cGroup
                         from c in cGroup.DefaultIfEmpty()
                         select new { p, pic, c }).AsQueryable();

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.StatusCategoryAssign))
            {
                switch(request.StatusCategoryAssign)
                {
                    case "true": query = query.Where(x => x.c.CategoryName != null); break;
                    case "false": query = query.Where(x => x.c.CategoryName == null); break;
                }
            }

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.ProductName.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                switch (request.OrderBy)
                {
                    case "nameAsc": query = query.OrderBy(x => x.p.ProductName); break;
                    case "latestProduct": query = query.OrderByDescending(x => x.p.CreatedAt); break;
                    case "priceAsc": query = query.OrderBy(x => x.p.DiscountPrice); break;
                    case "priceDesc": query = query.OrderByDescending(x => x.p.DiscountPrice); break;

                }
            }

            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                  .Select(x => new ProductVm()
                  {
                      Id = x.p.Id,
                      ProductName = x.p.ProductName,
                      DiscountPrice = x.p.DiscountPrice,
                      RegularPrice = x.p.RegularPrice,
                      CategoryName = x.c.CategoryName,
                      Brand = x.p.Brand,
                      Description = x.p.Description,
                      ImagePath = x.p.ImagePath,
                      Ingredients = x.p.Ingredients,
                      Manufacturer = x.p.Manufacturer,
                      Origin = x.p.Origin,
                      Packaging = x.p.Packaging,
                      ProductionLocation = x.p.ProductionLocation,
                      SeoAlias = x.p.SeoAlias,
                      SeoTitle = x.p.SeoTitle,
                      IsPrescriptionRequired = x.p.isPrescriptionRequired,
                      
                  }).ToListAsync();

            //select

            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }


        public async Task<bool> SoftDelete(int id)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {

                product.IsPublished = false;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"product{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>($"Sản phẩm với id {id} không tồn tại !");
            }
            foreach(var category in request.Categories)
            {
                var productInCategory = await _dbContext.ProductInCategories.FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id) && x.ProductId == id);
                if(productInCategory != null && category.IsSelected== false) {
                    _dbContext.ProductInCategories.Remove(productInCategory);
                }
                else if(productInCategory == null && category.IsSelected)
                {
                    await _dbContext.ProductInCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId= int.Parse(category.Id),
                        ProductId= id
                    });
                }
            }
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<bool> CreateBatch(BatchCreateRequest request)
        {
            var newBatch = new Batch()
            {
                ProductId = request.ProductId,
                OriginPrice = request.OriginPrice,
                Quantity = request.Quantity,
                Stock = request.Stock,
                ManufacturingDate = request.ManufacturingDate,
                ExpireDate = request.ExpireDate,
                IsDeleted = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _dbContext.Batches.Add(newBatch);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<BatchVm>> GetAllBatch(int productId)
        {
            var batchProduct = await _dbContext.Batches.Where(x => x.ProductId == productId).ToListAsync();
            var result = batchProduct.Select(x => new BatchVm()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                OriginPrice = x.Quantity,
                Quantity = x.Quantity,
                Stock = x.Stock,
                ManufacturingDate = x.ManufacturingDate,
                ExpireDate = x.ExpireDate,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });
            return new List<BatchVm>(result);
        }

        public async Task<bool> UpdateStock(BatchUpdateStockRequest request)
        {
            var existingBatchProduct= await _dbContext.Batches.FirstOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId);
            existingBatchProduct.Stock = request.Stock;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
