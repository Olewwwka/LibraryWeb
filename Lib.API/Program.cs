using FluentValidation;
using FluentValidation.AspNetCore;
using Lib.API.Extensions;
using Lib.API.Validators;
using Lib.Application.Services;
using Lib.Core.Abstractions;
using Lib.Infrastructure;
using Lib.Infrastructure.Identity;
using Lib.Infrastructure.Repositories;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    c.OperationFilter<FluentValidationOperationFilter>(); 
});

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(LibraryDbContext)));
});

services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

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
