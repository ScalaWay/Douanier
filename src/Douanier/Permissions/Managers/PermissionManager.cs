using Douanier.Abstractions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Managers;
using Douanier.Abstractions.Permissions.Stores;
using Douanier.Options;
using Douanier.Permissions.Stores;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace Douanier.Permissions.Managers
{
    public class PermissionManager : PermissionManager<PermissionGroup, Permission, Guid>
    {
        public PermissionManager(
            ILogger<PermissionManager> logger,
            IServiceProvider serviceProvider,
            DouanierOptions options,
            IPermissionGroupStore<PermissionGroup, Permission, Guid> permissionGroupStore,
            IPermissionStore<Permission, Guid> permissionStore) : base(logger, serviceProvider, options, permissionGroupStore, permissionStore)
        {
        }
    }

    /// <summary>
    /// PermissionManager
    /// </summary>
    /// <typeparam name="TPermissionGroup"></typeparam>
    /// <typeparam name="TPermission"></typeparam>
    public abstract class PermissionManager<TPermissionGroup, TPermission, TKey> : IPermissionManager<TPermissionGroup, TPermission, TKey>
        where TPermissionGroup : PermissionGroup
        where TPermission : Permission
    {
        protected readonly ILogger<PermissionManager> logger;

        protected readonly IPermissionGroupStore<TPermissionGroup, TPermission, TKey> permissionGroupStore;

        protected readonly IPermissionStore<TPermission, TKey> permissionStore;

        protected readonly DouanierOptions options;

        protected readonly IServiceProvider serviceProvider;

        protected PermissionManager(
            ILogger<PermissionManager> logger,
            IServiceProvider serviceProvider,
            DouanierOptions options,
            IPermissionGroupStore<TPermissionGroup, TPermission, TKey> permissionGroupStore,
            IPermissionStore<TPermission, TKey> permissionStore)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.options = options;
            this.permissionGroupStore = permissionGroupStore;
            this.permissionStore = permissionStore;
        }

        private Task CanCreateAsync()
        {
            // Injected validator can be used here.
            if (!permissionStore.GetType().IsAssignableFrom(typeof(MemoryPermissionStore)))
                throw new NotImplementedException("Permission creation is not available whithout persistant store.");
            return Task.FromResult(0);
        }

        /// <summary>
        /// Create and save a new permission definition in persistant store.
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<TPermission> CreateAsync(TPermission permission)
        {
            await CanCreateAsync();

            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            return await permissionStore.AddAsync(permission);
        }

        /// <summary>
        /// Retrieve a permission defintion by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<TPermission> GetByIdAsync(TKey id)
        {
            if (!permissionStore.GetType().IsAssignableFrom(typeof(MemoryPermissionStore)))
                throw new NotImplementedException("Retrieve permission by id is not available whithout persistant store.");

            return await permissionStore.GetByIdAsync(id);
        }

        /// <summary>
        /// Retrieve a permission defintion by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<TPermission> GetByNameAsync(string name)
        {
            return await permissionStore.GetByNameAsync(name);
        }

        /// <summary>
        /// Retrieve all permission definitions.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IReadOnlyList<TPermission>> GetAllAsync()
        {
            var permissions = await permissionStore.GellAllAsync();
            return permissions.ToImmutableList();
        }

        public virtual async Task<DouanierResult> UpdateGroupAsync(TKey permissionId, TKey groupId)
        {
            if (!permissionStore.GetType().IsAssignableFrom(typeof(MemoryPermissionStore)))
                throw new NotImplementedException("Add permission to a group is not available whithout persistant store.");

            return DouanierResult.Success;
        }

        public virtual async Task<DouanierResult> DeleteAsync(TKey id)
        {
            if (!permissionStore.GetType().IsAssignableFrom(typeof(MemoryPermissionStore)))
                throw new NotImplementedException("Delete permission by id is not available whithout persistant store.");

            try
            {
                await permissionStore.DeleteAsync(id);
                return DouanierResult.Success;
            }
            catch (Exception ex)
            {
                return new DouanierResult(ex.Message);
            }
        }

        public virtual async Task<DouanierResult> UpdateAsync(TPermission permission)
        {
            if (!permissionStore.GetType().IsAssignableFrom(typeof(MemoryPermissionStore)))
                throw new NotImplementedException("Update permission is not available whithout persistant store.");

            await permissionStore.UpdateAsync(permission);

            return DouanierResult.Success;
        }
    }
}