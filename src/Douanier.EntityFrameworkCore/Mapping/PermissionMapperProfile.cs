using AutoMapper;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.EntityFrameworkCore.Permissions.Models;

namespace Douanier.EntityFrameworkCore.Mapping
{
    /// <summary>
    /// Defines entity/model mapping for permissions.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class PermissionMapperProfile : Profile
    {
        public PermissionMapperProfile()
        {
            CreateMap<Permission, PermissionModel>(MemberList.None)
                .ReverseMap();
        }
    }
}