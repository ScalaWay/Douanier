using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Douanier.EntityFrameworkCore.Configuration
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionModel>
    {
        public void Configure(EntityTypeBuilder<PermissionModel> builder)
        {
            // Define table name.
            builder.ToTable("Permissions");

            builder.HasIndex(permission => permission.Name);
        }
    }
}