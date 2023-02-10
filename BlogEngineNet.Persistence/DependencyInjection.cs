using BlogEngineNet.Core;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineNet.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services,
            IConfiguration configuration)
    {

        services.AddCoreDependencies();
        services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("BlogEngineConnection"),
                     b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        return services;
    }
} 