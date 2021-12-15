using Microsoft.AspNetCore.Mvc;

namespace Douanier.Attributes
{
    public class DouanierRequirementAttribute : TypeFilterAttribute
    {
        public DouanierRequirementAttribute(string permission)
            : base(typeof(DouanierRequirementFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}