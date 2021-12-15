using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Stores;

namespace Douanier.Permissions.Stores
{
    public class MemoryPermissionGroupStore : IPermissionGroupStore<PermissionGroup, Permission, Guid>
    {
        private readonly IPermissionContext<PermissionGroup, Permission> permissionContext;

        public MemoryPermissionGroupStore(IPermissionContext<PermissionGroup, Permission> permissionContext)
        {
            this.permissionContext = permissionContext;
        }

        public Task<PermissionGroup> AddAsync(PermissionGroup group)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionGroup>> GellAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGroup> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGroup> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PermissionGroup group)
        {
            throw new NotImplementedException();
        }
    }
}