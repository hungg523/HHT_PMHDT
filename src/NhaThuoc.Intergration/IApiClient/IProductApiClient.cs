﻿using NhaThuoc.Application.Request.Common;
using NhaThuoc.Domain.ReQuest.Product;

namespace NhaThuoc.Intergration.IApiClient
{
    public interface IProductApiClient
    {
        Task<bool> Create(ProductCreateRequest request);

        Task<PagedResult<ProductBasicVm>> GetProductByCategoryIdPaging(GetPublicProductPagingRequest request);
        Task<PagedResult<ProductVm>> GetAllProductsPaging(GetManageProductPagingRequest request);

        Task<ProductVm> GetProductById(int id);

        //Task<bool> Update(int id, ProductUpdateRequest request);
        Task<bool> SoftDelete(int id);
    }
}
