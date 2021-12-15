using Douanier.Abstractions;
using Douanier.Extensions;
using Douanier.Options;
using Douanier.Permissions;
using Douanier.Permissions.Managers;
using Douanier.Permissions.Stores;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// DI extension methods for adding Lucius Authorization.
/// </summary>
public static class DouanierSetupExtensions
{
    /// <summary>
    /// Creates a builder.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    private static IDouanierBuilder AddLuciusAuthorizationBuilder(this IServiceCollection services)
    {
        return new DouanierBuilder(services);
    }

    /// <summary>
    /// Adds Lucius Authorization Library.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IDouanierBuilder AddLuciusAuthorization(this IServiceCollection services)
    {
        var builder = services.AddLuciusAuthorizationBuilder();

        builder.Services.AddOptions();

        builder.Services.AddSingleton(
            resolver => resolver.GetRequiredService<IOptions<DouanierOptions>>().Value);

        builder.AddPermissionContext<PermissionContext>();
        builder.AddPermissionProvider();

        builder.AddMemoryPermissionStore<MemoryPermissionStore>();
        builder.AddMemoryPermissionGroupStore<MemoryPermissionGroupStore>();
        builder.AddPermissionManager<PermissionManager>();

        return builder;
    }

    /// <summary>
    /// Adds Lucius Authorization Library.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="setupAction">The setup action.</param>
    /// <returns></returns>
    public static IDouanierBuilder AddLuciusAuthorization(
        this IServiceCollection services,
        Action<DouanierOptions> setupAction)
    {
        services.Configure(setupAction);

        return services.AddLuciusAuthorization();
    }
}