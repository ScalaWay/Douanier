using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions
{
    public interface IPermissionProvider<TPermissionGroup, TPermission>
        where TPermission : Permission
        where TPermissionGroup : PermissionGroup
    {
        IPermissionContext<TPermissionGroup, TPermission> PermissionContext { get; }

        void SetPermissions();
    }
}