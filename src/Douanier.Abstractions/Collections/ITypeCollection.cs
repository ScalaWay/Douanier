namespace Douanier.Abstractions.Collections
{
    /// <summary>
    /// A shortcut for <see cref="ITypeCollection{TBaseType}"/> to use object as base type.
    /// </summary>
    public interface ITypeCollection
    {
    }

    /// <summary>
    /// Extends <see cref="ICollection{Type}"/> to add restriction a specific base type.
    /// </summary>
    /// <typeparam name="TBaseType">Base Type of <see cref="Type"/>s in this collection</typeparam>
    public interface ITypeCollection<in TBaseType> : ICollection<Type>
    {
        /// <summary>
        /// Adds a type to list.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// Adds a type to list if it's not already in the list.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        bool TryAdd<T>() where T : TBaseType;

        /// <summary>
        /// Checks if a type exists in the list.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// Removes a type from list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Remove<T>() where T : TBaseType;
    }
}