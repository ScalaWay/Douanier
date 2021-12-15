using System.Diagnostics.CodeAnalysis;

namespace Douanier.Abstractions.Permissions.Entities
{
    public class PermissionGrant
    {
        private Permission permission;

        public PermissionGrant() { 
        
        }

        public Guid PermissionId { get; set; }

        /// <summary>
        /// The action that can be performed on a specific resource.
        /// </summary>
        public Permission Permission { 
            set;
            get;
        }

        public string? Origin { get; set; }

        public bool IsGranted { get; set; }

        [NotNull]
        public string? SubjectType { get; set; }

        [NotNull]
        public string? SubjectId { get; set; }

        [NotNull]
        public string? ResourceId { get; set; }
    }
}