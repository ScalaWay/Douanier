using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions.Managers
{
    public interface IPermissionManager<TPermissionGroup, TPermission, TKey>
        where TPermission : Permission
        where TPermissionGroup : PermissionGroup
    {
        Task<TPermission> CreateAsync(TPermission permission);

        Task<DouanierResult> DeleteAsync(TKey id);

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="id"/> or throws exception
        /// if there is no permission with given <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TPermission> GetByIdAsync(TKey id);

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or throws exception
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<TPermission> GetByNameAsync(string name);

        Task<IReadOnlyList<TPermission>> GetAllAsync();

        Task<DouanierResult> UpdateGroupAsync(TKey permissionId, TKey groupId);

        Task<DouanierResult> UpdateAsync(TPermission permission);
    }
}