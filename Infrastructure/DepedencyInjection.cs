using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AuthModules;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces.Auth;
using Infrastructure.Services.AuthModules;

namespace Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDatabaseConfig(configuration);
 
        }



      
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("constr").Value ??
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
