using Douanier.Abstractions.Permissions.Entities;

namespace Douanier.Abstractions.Permissions.Managers
{
    public interface IPermissionGrantManager<TPermission, TPermissionGrant>
        where TPermission : Permission
        where TPermissionGrant : PermissionGrant
    {
        Task<DouanierResult> AuthorizeAsync<T>(TPermission permission, string subjectId, string resourceId);

        /// <summary>
        /// Grant specific permission to a resource for a subject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="permission"></param>
        /// <param name="subjectId"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        Task<TPermissionGrant> GrantAsync<T>(TPermission permission, string subjectId, string resourceId);

        Task RevokeAsync<T>(TPermission permission, string subjectId, string resourceId);
    }
}