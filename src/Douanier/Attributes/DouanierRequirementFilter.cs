using Douanier.Abstractions.Permissions.Entities;
using Douanier.Abstractions.Permissions.Stores;
using Douanier.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Douanier.Attributes
{
    public class DouanierRequirementFilter : IAuthorizationFilter
    {
        private readonly DouanierOptions options;
        private readonly string permission;

        public DouanierRequirementFilter(DouanierOptions options, string permission)
        {
            this.options = options;
            this.permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Ensure an user is authenticated.
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Retrieve available role
            var roles = user
                .Identities
                .SelectMany(s => s.Claims)
                .Where(s => s.Type.Contains(options.RoleClaimType))
                .Select(s => s.Value)
                .FirstOrDefault();

            var permissionGrantStore = context
                .HttpContext
                .RequestServices
                .GetService(typeof(IPermissionGrantStore<Permission, PermissionGrant>));

            if (permissionGrantStore == null)
            {
                throw new InvalidOperationException("Unable to retrieve ");
            }
        }
    }
}