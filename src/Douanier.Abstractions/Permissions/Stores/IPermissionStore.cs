using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions.Stores
{
    public interface IPermissionStore<TPermission, TKey>
    {
        /// <summary>
        /// Gets all permissions.
        /// </summary>
        Task<IEnumerable<TPermission>> GellAllAsync();

        Task<TPermission> GetByNameAsync(string name);

        Task<TPermission> GetByIdAsync(TKey id);

        Task<IEnumerable<TPermission>> FindByGroupId<T>(T permissionGroupId);

        Task<TPermission> AddAsync(TPermission permission);

        Task DeleteAsync(TKey id);

        Task UpdateAsync(TPermission permission);
    }
}