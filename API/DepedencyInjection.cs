using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace Api

{
    public static class DepedencyInjection
    {

        public static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
           .AddControllersConfig()
           .AddSwaggerWithAuth();
            return services;

        }



        private static IServiceCollection AddControllersConfig(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
        {
            // ── Swagger ─────────────────────────────
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AcceptLanguageHeaderOperationFilter>();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication Module API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token with Bearer prefix (Example: 'Bearer eyJhbGciOi...')",
                });
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            return services;
        }

    }
}
