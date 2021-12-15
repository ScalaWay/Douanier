using AutoMapper;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.EntityFrameworkCore.Permissions.Models;

namespace Douanier.EntityFrameworkCore.Mapping
{
    /// <summary>
    /// Extension methods to map to/from entity/model for Permissions.
    /// </summary>
    public static class PermissionMappers
    {
        internal static IMapper Mapper { get; }

        static PermissionMappers()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<PermissionMapperProfile>();

                // Child object profiles
                // https://docs.automapper.org/en/stable/Nested-mappings.html
                cfg.AddProfile<PermissionGroupMapperProfile>();
            })
            .CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static PermissionModel? ToModel(this Permission entity)
        {
            return entity == null ? null : Mapper.Map<PermissionModel>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Permission? ToEntity(this PermissionModel model)
        {
            return model == null ? null : Mapper.Map<Permission>(model);
        }

        /// <summary>
        /// Updates an entity from a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="entity">The entity.</param>
        public static void UpdateEntity(this PermissionModel model, Permission entity)
        {
            Mapper.Map(model, entity);
        }
    }
}