using BlogEngineNet.Core.Services.Implementations;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineNet.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services )
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}  