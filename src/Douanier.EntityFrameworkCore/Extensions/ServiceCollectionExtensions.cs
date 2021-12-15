using Douanier.EntityFrameworkCore;
using Douanier.EntityFrameworkCore.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Douanier DbContext to the DI system.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction">The store options action.</param>
    /// <returns></returns>
    internal static IServiceCollection AddAuthorizationDbContext(
        this IServiceCollection services,
        Action<DouanierDbContextOptions>? optionsAction = null)
    {
        return services.AddAuthorizationDbContext<DouanierDbContext>(optionsAction);
    }

    /// <summary>
    /// Add Douanier DbContext to the DI system.
    /// </summary>
    /// <typeparam name="TContext">The IConfigurationDbContext to use.</typeparam>
    /// <param name="services"></param>
    /// <param name="optionsAction">The store options action.</param>
    /// <returns></returns>
    internal static IServiceCollection AddAuthorizationDbContext<TContext>(
        this IServiceCollection services,
        Action<DouanierDbContextOptions>? optionsAction = null)
        where TContext : DouanierDbContext, IDouanierDbContext
    {
        var options = new DouanierDbContextOptions();
        services.AddSingleton(options);

        optionsAction?.Invoke(options);

        if (options.ResolveDbContextOptions != null)
        {
            services.AddDbContext<TContext>(options.ResolveDbContextOptions);
        }
        else
        {
            services.AddDbContext<TContext>(dbCtxBuilder =>
            {
                options.ConfigureDbContext?.Invoke(dbCtxBuilder);
            });
        }
        services.AddScoped<IDouanierDbContext, TContext>();

        return services;
    }
}