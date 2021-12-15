using Douanier.Abstractions.Permissions;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Stores;
using Douanier.EntityFrameworkCore.Mapping;
using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Douanier.EntityFrameworkCore.Permissions.Stores
{
    public class PermissionGroupStore : IPermissionGroupStore<PermissionGroupModel, PermissionModel, Guid>
    {
        /// <summary>
        /// The DbContext.
        /// </summary>
        protected readonly IDouanierDbContext context;

        protected readonly IPermissionContext<PermissionGroup, Permission> permissionContext;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger<PermissionGroupStore> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionGroupStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public PermissionGroupStore(
            IDouanierDbContext context,
            IPermissionContext<PermissionGroup, Permission> permissionContext,
            ILogger<PermissionGroupStore> logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.permissionContext = permissionContext;
            this.logger = logger;
        }

        public async Task SyncPermissionGroupStoreWithContextAsync()
        {
            this.logger.LogDebug(
                "Synchronize {count} memory permission group definitions with persistant store.", 
                this.permissionContext.PermissionGroups.Values.Count);

            foreach (var group in this.permissionContext.PermissionGroups.Values)
            {
                try
                {
                    await GetByNameAsync(group.Name);
                    // Skip if a group already exists with this name
                    this.logger.LogDebug(
                        "Group {groupName} already exists in persistant store.", 
                        group.Name);
                    continue;
                } catch
                {
                    // Convert memory data into model and save it
                    var permissionGroupModel = group.ToModel();
                    if (permissionGroupModel == null)
                    {
                        this.logger.LogWarning(
                            "Failed to convert memory group {groupName} into persistant store model.", 
                            group.Name);
                        continue;
                    }

                    await AddAsync(permissionGroupModel);
                }
            }
        }

        public async Task<PermissionGroupModel> AddAsync(PermissionGroupModel group)
        {
            await context.PermissionGroups.AddAsync(group);
            await context.SaveChangesAsync();
            return group;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionGroupModel>> GellAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGroupModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionGroupModel> GetByNameAsync(string name)
        {
            var query = context.PermissionGroups.AsQueryable();
            var permissionGroup = (await query
                .Where(p => p.Name == name)
                .ToArrayAsync())
                .SingleOrDefault(p => p.Name == name);

            if (permissionGroup == null)
                throw new InvalidOperationException("permissionGroup");

            return permissionGroup;
        }

        public Task UpdateAsync(PermissionGroupModel permission)
        {
            throw new NotImplementedException();
        }
    }
}