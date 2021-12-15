using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Stores;
using Douanier.EntityFrameworkCore.Mapping;
using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Douanier.EntityFrameworkCore.Permissions.Stores
{
    public class PermissionStore : IPermissionStore<PermissionModel, Guid>
    {
        /// <summary>
        /// The DbContext.
        /// </summary>
        protected readonly IDouanierDbContext context;

        protected readonly IPermissionGroupStore<PermissionGroupModel, PermissionModel, Guid> permissionGroupStore;

        /// <summary>
        /// Memory context.
        /// </summary>
        protected readonly IPermissionContext<PermissionGroup, Permission> permissionContext;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger<PermissionStore> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public PermissionStore(
            IDouanierDbContext context,
            IPermissionGroupStore<PermissionGroupModel, PermissionModel, Guid> permissionGroupStore,
            IPermissionContext<PermissionGroup, Permission> permissionContext,
            ILogger<PermissionStore> logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.permissionGroupStore = permissionGroupStore ?? throw new ArgumentNullException(nameof(permissionGroupStore));
            this.permissionContext = permissionContext;
            this.logger = logger;
        }

        /// <summary>
        /// Synchronize data from memory store to persistant store.
        /// </summary>
        /// <returns></returns>
        public async Task SyncPermissionStoreWithContextAsync()
        {
            this.logger.LogDebug(
                "Synchronize {count} memory permission definitions with persistant store.",
                this.permissionContext.Permissions.Values.Count);

            foreach (var permission in this.permissionContext.Permissions.Values)
            {
                try
                {
                    await GetByNameAsync(permission.Name);
                    // Skip if a group already exists with this name
                    this.logger.LogDebug(
                        "Permission {permissionName} already exists in persistant store.",
                        permission.Name);
                    continue;
                } catch
                {
                    var permissionModel = permission.ToModel();
                    if (permissionModel == null)
                    {
                        this.logger.LogError(
                            "Failed to convert memory group {permissionName} into persistant store model.",
                            permission.Name);
                        continue;
                    }

                    if (permission.PermissionGroup != null)
                    {
                        try {
                            var group = await permissionGroupStore.GetByNameAsync(permission.PermissionGroup.Name);
                            if (group != null)
                                permissionModel.PermissionGroup = group;
                        } catch {
                            this.logger.LogWarning(
                                "A group is attached to memory permission but can't be found in persistant store. {groupName} will be created automaticaly.",
                                permission.PermissionGroup.Name);
                        }
                    }

                    await AddAsync(permissionModel);
                }
            }
        }

        public async Task<PermissionModel> AddAsync(PermissionModel permission)
        {
            await this.context.Permissions.AddAsync(permission);
            await this.context.SaveChangesAsync();
            return permission;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionModel>> FindByGroupId<T>(T permissionGroupId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PermissionModel>> GellAllAsync()
        {
            var query = this.context.Permissions.AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<PermissionModel> GetByIdAsync(Guid id)
        {
            var permission = await this.context.Permissions.FindAsync(id);
            if (permission == null)
                throw new InvalidOperationException("");

            return permission;
        }

        public async Task<PermissionModel> GetByNameAsync(string name)
        {
            var query = this.context.Permissions.AsQueryable();
            var permission = (await query
                .Where(p => p.Name == name)
                .ToArrayAsync())
                .SingleOrDefault(p => p.Name == name);

            if (permission == null)
                throw new InvalidOperationException();

            return permission;
        }

        public Task UpdateAsync(PermissionModel permission)
        {
            throw new NotImplementedException();
        }
    }
}