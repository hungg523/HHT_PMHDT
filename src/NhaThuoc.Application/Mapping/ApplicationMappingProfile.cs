using AutoMapper;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
using NhaThuoc.Application.Request.Order;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuoc.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            #region Category
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, CreateProductRequest>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();
            #endregion

            #region Coupon
            CreateMap<Coupon, CouponCreateRequest>().ReverseMap();
            CreateMap<Coupon, CouponUpdateRequest>().ReverseMap();
            #endregion

            #region Order
            CreateMap<Order, OrderCreateRequest>().ReverseMap();
            CreateMap<Order, OrderUpdateRequest>().ReverseMap();
            #endregion

            #region CustomerAddress
            CreateMap<CustomerAddress, CustomerAddressCreateRequest>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressUpdateRequest>().ReverseMap();
            #endregion

            #region Customer
            CreateMap<Customer, LoginRequest>().ReverseMap();
            CreateMap<Customer, RegisterRequest>().ReverseMap();
            #endregion
        }
    }
}