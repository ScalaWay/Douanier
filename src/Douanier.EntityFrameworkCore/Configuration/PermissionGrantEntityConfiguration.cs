using Douanier.EntityFrameworkCore.Permissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Douanier.EntityFrameworkCore.Configuration
{
    public class PermissionGrantEntityConfiguration : IEntityTypeConfiguration<PermissionGrantModel>
    {
        public void Configure(EntityTypeBuilder<PermissionGrantModel> builder)
        {
            // Define table name.
            builder.ToTable("PermissionGrants");

            //builder
            //    .HasDiscriminator(grant => grant.SubjectType)
            //    .HasValue("Account")
            //    .HasValue("Role")
            //    .HasValue("Team");

            // Configure one to many relationship with Permissions.
            builder
                .HasOne(grant => grant.Permission)
                .WithMany()
                .HasForeignKey(grant => grant.PermissionId);

            builder.HasKey(grant => new { grant.PermissionId, grant.SubjectId, grant.ResourceId });
        }
    }
}