using System.Diagnostics.CodeAnalysis;

namespace Douanier.EntityFrameworkCore.Permissions.Models
{
    public class PermissionGrantModel
    {
        public Guid PermissionId { get; set; }

        [NotNull]
        public PermissionModel? Permission { get; set; }

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