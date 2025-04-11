using Lib.Core.Abstractions.Repositories;
using Lib.Infrastructure.Repositories;

namespace Lib.API.Extensions
{
    public static class RepoExtention
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
