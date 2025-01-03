﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NhaThuoc.Data.Repositories;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Abtractions.IRepositories.Base;
using NhaThuoc.Share.Constant;

namespace NhaThuoc.Data.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NhaThuocOnlineDb")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserMessageRepository, UserMessageRepository>();
            services.AddScoped<IAdminMessageRepository, AdminMessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddSingleton<EmailService>();
            return services;
        }
    }
}