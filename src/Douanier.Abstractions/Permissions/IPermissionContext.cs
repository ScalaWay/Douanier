using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions
{
    public interface IPermissionContext<TPermissionGroup, TPermission>
        where TPermissionGroup : PermissionGroup
        where TPermission : Permission
    {
        /// <summary>
        /// Stores the permission group definition extracted from providers.
        /// </summary>
        Dictionary<string, TPermissionGroup> PermissionGroups { get; set; }

        /// <summary>
        /// Stores the permissions definition extracted from providers.
        /// </summary>
        Dictionary<string, TPermission> Permissions { get; set; }

        void Initialize();

        TPermission CreatePermission(
            string name,
            TPermissionGroup? permissionGroup = null,
            string? description = null);

        TPermissionGroup CreatePermissionGroup(
            string name,
            string? description = null);

        TPermission GetPermission(string name);

        TPermissionGroup GetPermissionGroup(string name);

        void RemovePermission(string name);

        void RemovePermissionGroup(string name);
    }
}