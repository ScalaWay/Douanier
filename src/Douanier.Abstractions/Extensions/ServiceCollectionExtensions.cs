using Microsoft.Extensions.DependencyInjection;

namespace Douanier.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Replace<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
            if (descriptorToRemove != null)
            {
                services.Remove(descriptorToRemove);
            }

            var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);

            services.Add(descriptorToAdd);

            return services;
        }

        public static IServiceCollection Replace<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            ServiceLifetime lifetime)
            where TService : class
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
            if (descriptorToRemove != null)
            {
                services.Remove(descriptorToRemove);
            }

            var descriptorToAdd = new ServiceDescriptor(typeof(TService), implementationFactory, lifetime);

            services.Add(descriptorToAdd);

            return services;
        }

        public static IServiceCollection Replace(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == serviceType);
            if (descriptorToRemove != null)
            {
                services.Remove(descriptorToRemove);
            }

            var descriptorToAdd = new ServiceDescriptor(serviceType, implementationType, lifetime);

            services.Add(descriptorToAdd);

            return services;
        }

    }


}
