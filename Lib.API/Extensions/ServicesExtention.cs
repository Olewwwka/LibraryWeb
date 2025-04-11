using Lib.Application;
using Lib.Core.Abstractions.Services;
using Lib.Core.Abstractions;
using Lib.Infrastructure.Services;
using Lib.Application.Services;
using Lib.Infrastructure.Identity;

namespace Lib.API.Extensions
{
    public static class ServicesExtention
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddHostedService<BooksOverdueService>();
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}
