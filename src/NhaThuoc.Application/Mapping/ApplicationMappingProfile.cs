using AutoMapper;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Validators.Categories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            //CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            //CreateMap<CreateCategoryRequest, CreateCategoryRequestValidator>().ReverseMap();
        }
    }
}