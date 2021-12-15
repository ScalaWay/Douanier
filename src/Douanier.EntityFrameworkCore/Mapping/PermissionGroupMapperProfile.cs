using AutoMapper;
using Douanier.Abstractions.Permissions.Entities;
using Douanier.EntityFrameworkCore.Permissions.Models;

namespace Douanier.EntityFrameworkCore.Mapping
{
    public class PermissionGroupMapperProfile : Profile
    {
        public PermissionGroupMapperProfile()
        {
            CreateMap<PermissionGroup, PermissionGroupModel>(MemberList.None)
                .ReverseMap();
        }
    }
}