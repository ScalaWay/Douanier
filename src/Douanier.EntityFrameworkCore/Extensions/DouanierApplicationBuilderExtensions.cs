using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.EntityFrameworkCore.Permissions.Stores;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class DouanierApplicationBuilderExtensions
{
    public static async Task<IApplicationBuilder> UseDouanierEntityFramework(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var permissionContext = scope.ServiceProvider.GetService<IPermissionContext<PermissionGroup, Permission>>();

            if (permissionContext == null)
                throw new ArgumentException(nameof(permissionContext));

            permissionContext.Initialize();

            // Synchronize data issued by static code with persistant storage.
            var permissionGroupStore = scope.ServiceProvider.GetRequiredService<PermissionGroupStore>();
            if (permissionGroupStore == null)
                throw new ArgumentException("Can't retrieve a Permission Group Store from DI container.");
            await permissionGroupStore.SyncPermissionGroupStoreWithContextAsync();

            // Synchronize data issued by static code with persistant storage.
            var permissionStore = scope.ServiceProvider.GetRequiredService<PermissionStore>();
            if (permissionStore == null)
                throw new ArgumentException("Can't retrieve a Permission Store from DI container.");
            await permissionStore.SyncPermissionStoreWithContextAsync();
        }

        return app;
    }
}