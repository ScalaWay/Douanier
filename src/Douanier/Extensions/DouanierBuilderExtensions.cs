using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Managers;
using Douanier.Abstractions.Permissions.Stores;
using Douanier.Options;
using Douanier.Permissions.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Douanier.Extensions
{
    public static class DouanierBuilderExtensions
    {
        public static IDouanierBuilder AddPermissionContext<T>(this IDouanierBuilder builder)
            where T : class, IPermissionContext<PermissionGroup, Permission>
        {
            builder.Services.AddSingleton<T>();
            builder.Services.AddSingleton<IPermissionContext<PermissionGroup, Permission>, T>();
            return builder;
        }

        public static IDouanierBuilder AddPermissionProvider(this IDouanierBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var optionService = serviceProvider.GetService<DouanierOptions>();

            if (optionService == null)
                throw new InvalidOperationException("Unable to resolve DouanierOptions from DI.");

            foreach (var type in optionService.Permission.PermissionProviders)
            {
                builder.Services.AddSingleton(type);
            }

            return builder;
        }

        public static IDouanierBuilder AddPermissionManager<T>(this IDouanierBuilder builder)
            where T : class, IPermissionManager<PermissionGroup, Permission, Guid>
        {
            builder.Services.AddTransient(typeof(T));
            builder.Services.AddTransient<IPermissionManager<PermissionGroup, Permission, Guid>, T>();
            return builder;
        }

        #region Stores

        public static IDouanierBuilder AddMemoryPermissionStore<T>(this IDouanierBuilder builder)
            where T : IPermissionStore<Permission, Guid>
        {
            builder.Services.AddTransient(typeof(T));
            builder.Services.AddTransient(typeof(IPermissionStore<Permission, Guid>), typeof(T));
            return builder;
        }

        public static IDouanierBuilder AddMemoryPermissionGroupStore<T>(this IDouanierBuilder builder)
            where T : MemoryPermissionGroupStore
        {
            builder.Services.AddTransient(typeof(T));
            builder.Services.AddTransient(typeof(IPermissionGroupStore<PermissionGroup, Permission, Guid>), typeof(T));
            return builder;
        }

        public static IDouanierBuilder AddMemoryPermissionGrantStore<T>(this IDouanierBuilder builder)
            where T : MemoryPermissionGrantStore
        {
            builder.Services.AddTransient(typeof(T));
            builder.Services.AddTransient(typeof(IPermissionGrantStore<,>), typeof(T));
            return builder;
        }

        #endregion Stores
    }
}