namespace Douanier.Options
{
    public class DouanierOptions
    {
        public DouanierOptions()
        {
        }

        public DouanierPermissionOptions Permission { get; set; } = new DouanierPermissionOptions();

        public string SubjectClaimType { get; set; } = "Sub";

        public string RoleClaimType { get; set; } = "Role";

        public string TeamClaimType { get; set; } = "Team";
    }
}