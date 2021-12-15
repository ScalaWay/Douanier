using AutoMapper;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.EntityFrameworkCore.Permissions.Models;

namespace Douanier.EntityFrameworkCore.Mapping
{
    public static class PermissionGroupMappers
    {
        internal static IMapper Mapper { get; }

        static PermissionGroupMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PermissionGroupMapperProfile>())
                .CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static PermissionGroupModel? ToModel(this PermissionGroup entity)
        {
            return entity == null ? null : Mapper.Map<PermissionGroupModel>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static PermissionGroup? ToEntity(this PermissionGroupModel model)
        {
            return model == null ? null : Mapper.Map<PermissionGroup>(model);
        }

        /// <summary>
        /// Updates an entity from a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="entity">The entity.</param>
        public static void UpdateEntity(this PermissionGroupModel model, PermissionGroup entity)
        {
            Mapper.Map(model, entity);
        }
    }
}