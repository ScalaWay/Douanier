namespace Douanier.Abstractions.Permissions.Stores
{
    public class PermissionGrantFilter
    {
        public Guid PermissionId { get; set; }

        public string SubjectId { get; set; } = String.Empty;

        public string? ResourceId { get; set; } = String.Empty;
    }
}