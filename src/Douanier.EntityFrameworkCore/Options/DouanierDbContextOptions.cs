using Microsoft.EntityFrameworkCore;

namespace Douanier.EntityFrameworkCore.Options
{
    /// <summary>
    /// Options for configuring the authorization context.
    /// </summary>
    public class DouanierDbContextOptions
    {
        /// <summary>
        /// Callback to configure the EF DbContext.
        /// </summary>
        /// <value>
        /// The configure database context.
        /// </value>
        public Action<DbContextOptionsBuilder>? ConfigureDbContext { get; set; }

        /// <summary>
        /// Callback in DI resolve the EF DbContextOptions. If set, ConfigureDbContext will not be used.
        /// </summary>
        /// <value>
        /// The configure database context.
        /// </value>
        public Action<IServiceProvider, DbContextOptionsBuilder>? ResolveDbContextOptions { get; set; }
    }
}