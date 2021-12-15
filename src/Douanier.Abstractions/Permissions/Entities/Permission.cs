using System.Diagnostics.CodeAnalysis;

namespace Douanier.Abstractions.Permissions.Entities
{
    public class Permission
    {

        public string? Description { get; set; }

        public string? DisplayName { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsStatic { get; set; } = false;

        public bool IsEnabled { get; set; } = true;

        public PermissionGroup? PermissionGroup { get; set; }
        public Guid PermissionGroupId { get; set; }

        public Permission(string name, string? displayName = null, string? description = null)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Description = description;
        }
    }
}