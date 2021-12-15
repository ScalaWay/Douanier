using System.Diagnostics.CodeAnalysis;

namespace Douanier.EntityFrameworkCore.Permissions.Models
{
    public class PermissionModel
    {
        #region Entity Properties

        public string? Description { get; set; }

        public string? DisplayName { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsStatic { get; set; }

        #endregion Entity Properties

        #region Navigation Properties

        public PermissionGroupModel? PermissionGroup { get; set; }
        public Guid PermissionGroupId { get; set; }

        #endregion Navigation Properties

        public PermissionModel(string name, string? displayName = null, string? description = null)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Description = description;
        }
    }
}