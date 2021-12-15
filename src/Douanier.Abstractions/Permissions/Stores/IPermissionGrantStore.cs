using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions.Stores
{
    public interface IPermissionGrantStore<TPermission, TPermissionGrant>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="permissionGrant"></param>
        /// <returns></returns>
        Task<TPermissionGrant> AddAsync(TPermissionGrant permissionGrant);

        Task DeleteAsync<T>(
            TPermission permission,
            string subjectId,
            string? resourceId);

        /// <summary>
        /// Get all grants.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TPermissionGrant>> GellAllAsync();

        /// <summary>
        /// Find any grants which match filter for a specific subject type.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<TPermissionGrant>> FindAsync<T>(PermissionGrantFilter filter);

        /// <summary>
        /// Find a permission granted to a specific subject type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="permission"></param>
        /// <param name="subjectId"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        Task<TPermissionGrant?> FindOneAsync<T>(
            TPermission permission,
            string subjectId,
            string? resourceId);

        Task UpdateAsync(TPermissionGrant permission);
    }
}