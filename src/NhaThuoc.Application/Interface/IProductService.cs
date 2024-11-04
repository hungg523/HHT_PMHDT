using NhaThuoc.Domain.ReQuest.Batch;
using NhaThuoc.Domain.ReQuest.Common;
using NhaThuoc.Domain.ReQuest.Product;

namespace NhaThuoc.Application.Interface
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);

        //Task<ApiResult<bool>> Update(ProductUpdateRequest request);

        Task<bool> SoftDelete(int id);

        Task<PagedResult<ProductBasicVm>> GetAllByCategoryIdPaging(GetPublicProductPagingRequest request);

        Task<PagedResult<ProductVm>> GetAllProductsPaging (GetManageProductPagingRequest request);

        Task<ProductVm> GetProductById(int id);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        //lô hàng
        Task<bool> CreateBatch(BatchCreateRequest request);
        Task<List<BatchVm>> GetAllBatch(int productId);

        Task<bool> UpdateStock(BatchUpdateStockRequest request);


    }
}
