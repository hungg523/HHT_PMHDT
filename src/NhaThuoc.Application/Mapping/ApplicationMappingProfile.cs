using AutoMapper;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Application.Request.Coupon;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Request.Customers.CustomerAddress;
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
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();
            CreateMap<Product, GetByNameProductRequest>().ReverseMap();
            #endregion

            #region Coupon
            CreateMap<Coupon, CouponCreateRequest>().ReverseMap();
            CreateMap<Coupon, CouponUpdateRequest>().ReverseMap();
            CreateMap<Coupon, CouponDTO>().ReverseMap();
            #endregion

            #region Order
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.CustomerAddress))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.Coupon, opt => opt.MapFrom(src => src.Coupon))
                .ReverseMap()
                .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Coupon, opt => opt.MapFrom(src => src.Coupon))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<CustomerAddress, CustomerAddressDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            #endregion

            #region CustomerAddress
            CreateMap<CustomerAddress, CustomerAddressCreateRequest>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressUpdateRequest>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressDTO>().ReverseMap();
            #endregion

            #region Customer
            CreateMap<Customer, LoginRequest>().ReverseMap();
            CreateMap<Customer, RegisterRequest>().ReverseMap();
            CreateMap<Customer, AuthenCustomerRequest>().ReverseMap();
            CreateMap<Customer, ChangePasswordRequest>().ReverseMap();
            CreateMap<Customer, UpdateCustomerPasswordRequest>().ReverseMap();
            #endregion
        }
    }
}