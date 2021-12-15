using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;

namespace Douanier.EntityFrameworkCore
{
    /// <summary>
    /// Abstraction for the authorization context.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDouanierDbContext : IDouanierDbContext<PermissionModel, PermissionGroupModel, PermissionGrantModel>
    {
    }

    public interface IDouanierDbContext<TPermission, TPermissionGroup, TAccountPermissionGrant> : IDisposable
        where TPermission : class
        where TPermissionGroup : class
        where TAccountPermissionGrant : class
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Gets or sets the permissions definitions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        DbSet<TPermission> Permissions { get; }

        /// <summary>
        /// Gets or sets the identity resources.
        /// </summary>
        /// <value>
        /// The identity resources.
        /// </value>
        DbSet<TPermissionGroup> PermissionGroups { get;}

        /// <summary>
        /// Gets or sets the permission grants.
        /// </summary>
        /// <value>
        /// The API resources.
        /// </value>
        DbSet<TAccountPermissionGrant> PermissionGrants { get; }
    }
}