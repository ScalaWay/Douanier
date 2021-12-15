using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Stores;

namespace Douanier.Permissions.Stores
{
    public class MemoryPermissionStore : IPermissionStore<Permission, Guid>
    {
        private readonly IPermissionContext<PermissionGroup, Permission> permissionContext;

        public MemoryPermissionStore(IPermissionContext<PermissionGroup, Permission> permissionContext)
        {
            this.permissionContext = permissionContext;
        }

        public Task<Permission> AddAsync(Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> FindByGroupId<T>(T permissionGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GellAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Permission> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Permission permission)
        {
            throw new NotImplementedException();
        }
    }
}