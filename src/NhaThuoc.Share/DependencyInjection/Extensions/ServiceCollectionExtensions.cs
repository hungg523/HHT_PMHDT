using Microsoft.Extensions.DependencyInjection;
using NhaThuoc.Share.Service;

namespace NhaThuoc.Share.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}