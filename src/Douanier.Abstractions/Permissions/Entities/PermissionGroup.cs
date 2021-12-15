using System.Collections.ObjectModel;

namespace Douanier.Abstractions.Permissions.Entities
{
    public class PermissionGroup
    {
        public virtual string Name { get; set; }

        public virtual string? Description { get; set; }

        public virtual Guid Id { get; set; }

        public virtual bool IsStatic { get; set; }

        public IEnumerable<Permission>? Permissions { get; set; }

        public PermissionGroup(string name, string? description = null)
        {
            this.Name = name;
            this.Description = description;
            this.Permissions = new Collection<Permission>();
        }
    }
}