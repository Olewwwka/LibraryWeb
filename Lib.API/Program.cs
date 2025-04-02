using Lib.API.Extensions;
using Lib.Application.Services;
using Lib.Core.Abstractions;
using Lib.Infrastructure;
using Lib.Infrastructure.Identity;
using Lib.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(LibraryDbContext)));
});

services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));


services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IUsersService, UsersService>();
services.AddScoped<IAuthorsService, AuthorsService>();
services.AddScoped<IBooksService, BooksService>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<ITokenService, TokenService>();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
