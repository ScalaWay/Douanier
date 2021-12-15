using Douanier.Abstractions.Permissions.Stores;
using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Douanier.EntityFrameworkCore.Permissions.Stores
{
    public class PermissionGrantStore : IPermissionGrantStore<PermissionModel, PermissionGrantModel>
    {
        /// <summary>
        /// The DbContext.
        /// </summary>
        protected readonly IDouanierDbContext context;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger<PermissionGrantStore> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionGrantStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public PermissionGrantStore(
            IDouanierDbContext context,
            ILogger<PermissionGrantStore> logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;
        }

        public async Task<PermissionGrantModel> AddAsync(PermissionGrantModel permissionGrant)
        {
            await this.context.PermissionGrants.AddAsync(permissionGrant);
            await this.context.SaveChangesAsync();
            return permissionGrant;
        }

        public async Task DeleteAsync<T>(PermissionModel permission, string subjectId, string? resourceId)
        {
            var grant = await FindOneAsync<T>(permission, subjectId, resourceId);
            if (grant != null)
            {
                this.context.PermissionGrants.Remove(grant);

                try
                {
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.logger.LogWarning("exception removing permission grant from database: {error}", ex.Message);
                }
            }
            else
            {
                this.logger.LogDebug("");
            }
        }

        public async Task<IEnumerable<PermissionGrantModel>> FindAsync<T>(PermissionGrantFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            var grants = await context
                .PermissionGrants
                .Where(prop =>
                    prop.ResourceId == filter.ResourceId &&
                    prop.PermissionId == filter.PermissionId &&
                    prop.SubjectId == filter.SubjectId &&
                    prop.SubjectType == nameof(T))
                .ToArrayAsync();

            return grants;
        }

        public async Task<PermissionGrantModel?> FindOneAsync<T>(PermissionModel permission, string subjectId, string? resourceId)
        {
            if (permission == null) throw new ArgumentNullException(nameof(permission));
            if (subjectId == null) throw new ArgumentNullException(nameof(subjectId));

            var query = this.context.PermissionGrants.AsNoTracking()
                .Where(grant =>
                    grant.Permission.Equals(permission) &&
                    grant.SubjectId == subjectId.ToString() &&
                    grant.SubjectType == nameof(T));

            if (!String.IsNullOrEmpty(resourceId))
            {
                query = query.Where(grant => grant.ResourceId == resourceId);
            }

            var permissionGrant = (await query
                    .ToArrayAsync())
                    .SingleOrDefault();

            this.logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", permission.Name, permissionGrant != null);

            return permissionGrant;
        }

        public async Task<IEnumerable<PermissionGrantModel>> GellAllAsync()
        {
            var query = this.context.PermissionGrants.AsQueryable();

            return await query.ToListAsync();
        }

        public Task UpdateAsync(PermissionGrantModel permission)
        {
            throw new NotImplementedException();
        }
    }
}