using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProductAssemblySystem.UserManagement.Infrastructure.Authentication.Interfaces;
using ProductAssemblySystem.UserManagement.Infrastructure.Authentication;
using ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces;
using ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Repositories;
using ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore;
using ProductAssemblySystem.UserManagement.Application.Interfaces;
using ProductAssemblySystem.UserManagement.Application.Services;
using ProductAssemblySystem.UserManagement.Domain.Enums;
using System.Text;
using ProductAssemblySystem.UserManagement.Infrastructure.Authorization;
using ProductAssemblySystem.UserManagement.Infrastructure.Authorization.Interfaces;

namespace ProductAssemblySystem.API.Extensions
{
    public static class CustomServiceCollectionExtensions
    {
        public static void AddUserManagementDbContext(this IServiceCollection services)
        {
            services.AddDbContext<UserManagementDbContext>();
        }

        public static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtOptions jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>()!;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["cookies"];
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void ConfigureAuthorizationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
        }

        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services
                .AddAuthorizationBuilder()
                .AddPolicy("ReadingPolicy", policy => policy.Requirements.Add(new PermissionRequirement([PermissionEnum.Read])));
        }

        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserManagementDbContext();

            services.ConfigureJwtOptions(configuration);
            services.AddCustomAuthentication(configuration);

            services.ConfigureAuthorizationOptions(configuration);
            services.AddCustomAuthorization();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddScoped<IJwtProvider, JwtProvider>();
        }
    }
}
