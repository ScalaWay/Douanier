using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Permissions
{
    public abstract class PermissionProvider
    {
        public abstract void SetPermissions(IPermissionContext<PermissionGroup, Permission> permissionContext);
    }
}