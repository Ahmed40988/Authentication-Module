using Application.Interfaces;
using Application.Interfaces.Auth;
using Domain.Entities.AuthModules;
using Infrastructure.Localizer;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services.AuthModules;
using Infrastructure.Services.Email;
using Infrastructure.Services.Hasher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using ServiceStack.Auth;

namespace Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(
    configuration.GetSection("MailSettings"));
            return services
                .AddDatabaseConfig(configuration)
                .AddPersistence()
                    .AddLocalizationConfig()
                .AddIdentityConfig();


        }
        // ── Localization ─────────────────────────────
        private static IServiceCollection AddLocalizationConfig(
            this IServiceCollection services)
        {
            services.AddLocalization();

            services.AddScoped<IStringLocalizer, JsonStringLocalizer>();

            return services;
        }



        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
           // services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService , EmailService>();
            services.AddScoped<IApplicationDbContext>(provider =>
                 provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IHasherService, HasherService>();
            services.AddScoped<IGetCurrentUserRepository, GetCurrentUserRepository>();

        

            return services;
        }
        private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {

            services.AddIdentityCore<User>()
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
            return services;
        }

           }
}
