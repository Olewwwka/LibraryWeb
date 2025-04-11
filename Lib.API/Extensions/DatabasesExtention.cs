using Lib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Lib.API.Extensions
{
    public static class DatabasesExtention
    {
        public static void AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(LibraryDbContext)),
                npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

        }
    }
}
