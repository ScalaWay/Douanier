using Douanier.Abstractions.Extensions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Stores;
using Douanier.EntityFrameworkCore;
using Douanier.EntityFrameworkCore.Options;
using Douanier.EntityFrameworkCore.Permissions.Models;
using Douanier.EntityFrameworkCore.Permissions.Stores;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods to add EF database support to Douanier.
/// </summary>
public static class DouanierBuilderExtensions
{
    /// <summary>
    /// Configures EF implementation of IPermissionStore, IPermissionGroupStore, and IPermissionGrantStore
    /// with Douanier.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="storeOptionsAction">The store options action.</param>
    /// <returns></returns>
    public static IDouanierBuilder AddAuthorizationContext(
        this IDouanierBuilder builder,
        Action<DouanierDbContextOptions>? options = null)
    {
        return builder.AddAuthorizationContext<DouanierDbContext>(options);
    }

    /// <summary>
    /// Configures EF implementation of IPermissionStore, IPermissionGroupStore, and IPermissionGrantStore
    /// with Douanier.
    /// </summary>
    /// <typeparam name="TContext">The DouanierDbContext to use.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="storeOptionsAction">The store options action.</param>
    /// <returns></returns>
    public static IDouanierBuilder AddAuthorizationContext<TContext>(
        this IDouanierBuilder builder,
        Action<DouanierDbContextOptions>? options = null)
        where TContext : DouanierDbContext
    {
        builder.Services.AddAuthorizationDbContext<TContext>(options);
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.AddPermissionGroupStore<PermissionGroupStore>();
        builder.AddPermissionStore<PermissionStore>();

        return builder;
    }

    public static IDouanierBuilder AddPermissionStore<T>(this IDouanierBuilder builder)
        where T : class, IPermissionStore<PermissionModel, Guid>
    {
        builder.Services.AddTransient(typeof(T));
        //builder.Services.Replace(typeof(IPermissionStore<Permission,Guid>), typeof(T), ServiceLifetime.Transient);
        builder.Services.AddTransient<IPermissionStore<PermissionModel, Guid>, T>();
        return builder;
    }

    public static IDouanierBuilder AddPermissionGroupStore<T>(this IDouanierBuilder builder)
        where T : class, IPermissionGroupStore<PermissionGroupModel, PermissionModel, Guid>
    {
        builder.Services.AddTransient(typeof(T));
        //builder.Services.Replace(typeof(IPermissionGroupStore<PermissionGroup, Permission, Guid>), typeof(T), ServiceLifetime.Transient);
        builder.Services.AddTransient<IPermissionGroupStore<PermissionGroupModel, PermissionModel, Guid>, T>();
        return builder;
    }

    public static IDouanierBuilder AddPermissionGrantStore<T>(this IDouanierBuilder builder)
        where T : class
    {
        builder.Services.AddTransient(typeof(T));
        builder.Services.Replace(typeof(IPermissionGrantStore<,>), typeof(T), ServiceLifetime.Transient);
        return builder;
    }
}