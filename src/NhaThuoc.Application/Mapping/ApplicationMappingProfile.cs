using AutoMapper;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            #region Category
            CreateMap<Category, CreateCategoryRequest > ().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest > ().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, CreateProductRequest>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();
            #endregion
        }
    }
}