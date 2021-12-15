using Douanier.Abstractions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Managers;
using Douanier.Abstractions.Permissions.Stores;
using Microsoft.Extensions.Logging;

namespace Douanier.Permissions.Managers
{
    public class PermissionGrantManager : PermissionGrantManager<Permission, PermissionGrant>
    {
        public PermissionGrantManager(
            ILogger<PermissionGrantManager> logger,
            IPermissionGrantStore<Permission, PermissionGrant> permissionGrantStore) : base(logger, permissionGrantStore)
        {
        }
    }

    public abstract class PermissionGrantManager<TPermission, TPermissionGrant> : IPermissionGrantManager<TPermission, TPermissionGrant>
        where TPermission : Permission
        where TPermissionGrant : PermissionGrant
    {
        protected readonly ILogger<PermissionGrantManager> logger;

        protected readonly IPermissionGrantStore<TPermission, TPermissionGrant> permissionGrantStore;

        protected PermissionGrantManager(
            ILogger<PermissionGrantManager> logger,
            IPermissionGrantStore<TPermission, TPermissionGrant> permissionGrantStore
            )
        {
            this.logger = logger;
            this.permissionGrantStore = permissionGrantStore;
        }

        public async Task<DouanierResult> AuthorizeAsync<T>(TPermission permission, string subjectId, string resourceId)
        {
            var grant = await this.permissionGrantStore.FindOneAsync<T>(permission, subjectId, resourceId);
            if (grant == null)
                return new DouanierResult("No authorization found.");

            if (!grant.IsGranted)
                return new DouanierResult("Not granted.");

            return DouanierResult.Success;
        }

        public async Task<TPermissionGrant> GrantAsync<T>(TPermission permission, string subjectId, string resourceId)
        {
            var grant = new PermissionGrant()
            {
                IsGranted = true,
                SubjectId = subjectId,
                ResourceId = resourceId,
                Permission = permission,
                SubjectType = nameof(T)
            } as TPermissionGrant;

            if (grant == null)
                throw new InvalidCastException();

            return await this.permissionGrantStore.AddAsync(grant);
        }

        public async Task RevokeAsync<T>(TPermission permission, string subjectId, string resourceId)
        {
            var grant = await this.permissionGrantStore.FindOneAsync<T>(permission, subjectId, resourceId);
            if (grant == null)
            {
                throw new InvalidOperationException();
            }

            grant.IsGranted = false;

            throw new NotImplementedException();
        }
    }
}