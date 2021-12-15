using Douanier.Abstractions.Collections;
using Douanier.Permissions;

namespace Douanier.Options
{
    public class DouanierPermissionOptions
    {
        /// <summary>
        /// Read-only typed collections of permissions providers speficied
        /// when the lib is configured.
        /// </summary>
        public ITypeCollection<PermissionProvider> PermissionProviders { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DouanierPermissionOptions()
        {
            PermissionProviders = new TypeCollection<PermissionProvider>();
        }
    }
}