using FluentValidation;
using FluentValidation.AspNetCore;
using Lib.API.Extensions;
using Lib.API.Middlewares;
using Lib.API.Validators;
using Lib.Core.Abstractions;
using Lib.Infrastructure;
using Lib.Infrastructure.Identity;
using Lib.Infrastructure.Repositories;
using Lib.Infrastructure.Services;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Microsoft.Extensions.FileProviders;
using Lib.Application;
using Library.Infrastructure.Data;
using Lib.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "http://localhost:80",
            "http://localhost:5000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{ 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    c.OperationFilter<FluentValidationOperationFilter>(); 
});

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(LibraryDbContext)),
        npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<ITokenService, TokenService>();

services.AddScoped<IBooksRepository, BooksRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();


services.AddScoped<INotificationService, NotificationService>();
services.AddHostedService<BooksOverdueService>();

services.AddUseCases();

services.AddAutoMapper(typeof(UserProfile).Assembly);

services.AddScoped<IFileService, FileService>();

var app = builder.Build();

var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

var defaultImagePath = Path.Combine(uploadsPath, "default_image.jpg");
if (!File.Exists(defaultImagePath))
{
    var sourceImagePath = Path.Combine(builder.Environment.ContentRootPath, "Uploads", "default_image.jpg");
    if (File.Exists(sourceImagePath))
    {
        File.Copy(sourceImagePath, defaultImagePath);
    }
}

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<LibraryDbContext>();
    InitDatabase.Initialize(context);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/api/uploads"
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
