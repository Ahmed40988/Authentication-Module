using Application.Validators.AuthModules;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
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

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(RegisterUserCommandValidator).Assembly);

            return services;
        }


    }
}
