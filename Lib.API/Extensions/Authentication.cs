using Lib.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lib.API.Extensions
{
    public static class Authentication
    {
        public static void AddApiAutorization(this IServiceCollection services,
           IConfiguration configuration, IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.TokenValidationParameters = new()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = jwtOptions.Value.Issuer,
                       ValidateAudience = true,
                       ValidAudience = jwtOptions.Value.Audience,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey
                           (Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           context.Token = context.Request.Cookies["jwtToken"];

                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy =>
                    policy.RequireRole("User"));
            });
        }
    }
}
