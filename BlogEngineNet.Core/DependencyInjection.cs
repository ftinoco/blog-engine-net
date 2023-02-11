using BlogEngineNet.Core.Services.Implementations;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineNet.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services )
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPostTrackingService, PostTrackingService>();
        return services;
    }
}  