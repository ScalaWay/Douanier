using Douanier.Abstractions.Extensions;
using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Douanier.Permissions
{
    public class PermissionContext : IPermissionContext<PermissionGroup, Permission>
    {
        protected readonly DouanierOptions options;

        protected readonly IServiceProvider serviceProvider;

        public Dictionary<string, Permission> Permissions { get; set; }

        public Dictionary<string, PermissionGroup> PermissionGroups { get; set; }

        public PermissionContext(
            IServiceProvider serviceProvider,
            DouanierOptions options)
        {
            this.serviceProvider = serviceProvider;
            this.options = options;

            Permissions = new Dictionary<string, Permission>();
            PermissionGroups = new Dictionary<string, PermissionGroup>();
        }

        public void Initialize()
        {
            foreach (var type in options.Permission.PermissionProviders)
            {
                var provider = serviceProvider.GetRequiredService(type) as PermissionProvider;
                if (provider == null)
                {
                    continue;
                }
                provider.SetPermissions(this);
            }
        }

        public Permission CreatePermission(
            string name,
            PermissionGroup? permissionGroup = null,
            string? description = null)
        {
            if (Permissions.ContainsKey(name))
            {
                throw new ArgumentException("There is already a permission with name: " + name);
            }

            var permission = new Permission(name) { PermissionGroup = permissionGroup };
            Permissions.Add(permission.Name, permission);

            return permission;
        }

        public PermissionGroup CreatePermissionGroup(string name, string? description = null)
        {
            if (PermissionGroups.ContainsKey(name))
            {
                throw new ArgumentException("There is already a permission group with name: " + name);
            }

            var permissionGroup = new PermissionGroup(name) { Description = description };
            PermissionGroups.Add(permissionGroup.Name, permissionGroup);

            return permissionGroup;
        }

        public Permission GetPermission(string name)
        {
            return Permissions.GetOrDefault(name);
        }

        public PermissionGroup GetPermissionGroup(string name)
        {
            return PermissionGroups.GetOrDefault(name);
        }

        public void RemovePermission(string name)
        {
            Permissions.Remove(name);
        }

        public void RemovePermissionGroup(string name)
        {
            PermissionGroups.Remove(name);
        }
    }
}