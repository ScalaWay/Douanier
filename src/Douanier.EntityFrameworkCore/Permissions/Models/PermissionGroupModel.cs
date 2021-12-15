using System.Collections.ObjectModel;

namespace Douanier.EntityFrameworkCore.Permissions.Models
{
    public class PermissionGroupModel
    {
        public virtual string Name { get; set; }

        public virtual string? Description { get; set; }

        public virtual Guid Id { get; set; }

        public virtual bool IsStatic { get; set; }

        public virtual IEnumerable<PermissionModel>? Permissions { get; set; }

        public PermissionGroupModel(string name, string? description = null)
        {
            this.Name = name;
            this.Description = description;
            this.Permissions = new Collection<PermissionModel>();
        }
    }
}