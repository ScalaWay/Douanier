using Douanier.EntityFrameworkCore.Options;
using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Douanier.EntityFrameworkCore
{
    public class DouanierDbContext : DouanierDbContext<PermissionModel, PermissionGroupModel, PermissionGrantModel>, IDouanierDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DouanierDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public DouanierDbContext(
            DbContextOptions options,
            DouanierDbContextOptions storeOptions)
            : base(options, storeOptions)
        {
        }
    }

    public abstract class DouanierDbContext<TPermission, TPermissionGroup, TPermissionGrant> : DbContext, IDouanierDbContext<TPermission, TPermissionGroup, TPermissionGrant>
        where TPermission : class
        where TPermissionGroup : class
        where TPermissionGrant : class
    {
        private readonly DouanierDbContextOptions douanierDbContextOptions;

        protected DouanierDbContext(
            DbContextOptions options,
            DouanierDbContextOptions douanierDbContextOptions) : base(options)
        {
            this.douanierDbContextOptions = douanierDbContextOptions;
        }

        public virtual DbSet<TPermission> Permissions => Set<TPermission>();

        public virtual DbSet<TPermissionGroup> PermissionGroups => Set<TPermissionGroup>();

        public virtual DbSet<TPermissionGrant> PermissionGrants => Set<TPermissionGrant>();

        public virtual Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}