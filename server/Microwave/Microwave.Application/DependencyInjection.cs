using Microwave.Application.Authentication.Services;
using Microwave.Application.Microwaves.Services;

namespace Microwave.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
        services.AddScoped<IMicrowaveService, MicrowaveService>();

        return services;
    }
}