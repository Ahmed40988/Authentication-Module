using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DepedencyInjection));
            });

            return services;
        }


    }
}
