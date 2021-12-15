using Microsoft.Extensions.DependencyInjection;

namespace Douanier.Abstractions
{
    /// <summary>
    /// Lucius Authorization helper class for DI configuration
    /// </summary>
    public class DouanierBuilder : IDouanierBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DouanierBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <exception cref="System.ArgumentNullException">services</exception>
        public DouanierBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public IServiceCollection Services { get; }
    }
}