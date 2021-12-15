using Douanier.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Douanier.Extensions
{
    public static class DouanierConfigurationExtension
    {
        public static IApplicationBuilder UseDouanier(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var permissionContext = scope.ServiceProvider.GetService<PermissionContext>();

                if (permissionContext == null)
                    throw new ArgumentException(nameof(permissionContext));

                permissionContext.Initialize();
            }

            return app;
        }
    }
}