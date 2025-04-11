using FluentValidation;
using FluentValidation.AspNetCore;
using Lib.API.Extensions;
using Lib.API.Middlewares;
using Lib.API.Validators;
using Lib.Infrastructure.Identity;
using Lib.Infrastructure.Services;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Lib.Application.Mappers;
using Lib.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{ 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    c.OperationFilter<FluentValidationOperationFilter>(); 
});

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.Configure<SMPTSettings>(builder.Configuration.GetSection("SmtpSettings"));

services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());


services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();


services.AddUseCases();
services.AddServices();
services.AddRepositories();
services.AddDb(configuration);

services.AddAutoMapper(typeof(UserProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    db.Database.Migrate();
}

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
