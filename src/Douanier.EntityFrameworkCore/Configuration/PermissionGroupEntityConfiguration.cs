using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Douanier.EntityFrameworkCore.Configuration
{
    public class PermissionGroupEntityConfiguration : IEntityTypeConfiguration<PermissionGroupModel>
    {
        public void Configure(EntityTypeBuilder<PermissionGroupModel> builder)
        {
            // Define table name.
            builder.ToTable("PermissionGroups");

            builder.HasIndex(group => group.Name);

            // Configure one to many relationship with Permission
            builder
                .HasMany(group => group.Permissions)
                .WithOne(permission => (PermissionGroupModel)permission.PermissionGroup)
                .HasForeignKey(g => g.PermissionGroupId);
        }
    }
}