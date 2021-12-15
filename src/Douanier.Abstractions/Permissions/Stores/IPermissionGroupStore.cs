using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions.Stores
{
    public interface IPermissionGroupStore<TPermissionGroup, TPermission, TKey>
    {
        Task<TPermissionGroup> GetByIdAsync(TKey id);

        Task DeleteAsync(TKey id);

        Task<IEnumerable<TPermissionGroup>> GellAllAsync();

        Task UpdateAsync(TPermissionGroup group);

        Task<TPermissionGroup> AddAsync(TPermissionGroup group);

        Task<TPermissionGroup> GetByNameAsync(string name);
    }
}