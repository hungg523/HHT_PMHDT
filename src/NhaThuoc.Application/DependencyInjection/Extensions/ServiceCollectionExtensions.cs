using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NhaThuoc.Share.DependencyInjection.Extensions;

namespace NhaThuoc.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
            services.AddHttpContextAccessor();
            services.AddFileServices();
            return services;
        }
    }
}