namespace MADE.Web.Identity
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Defines a base model for an authenticated user within the application.
    /// </summary>
    public class AuthenticatedUser
    {
        /// <summary>
        /// The value associated with the authenticated user's identity.
        /// </summary>
        public const string SubjectClaimType = "sub";

        /// <summary>
        /// The value associated with the authenticated user's preferred email address.
        /// </summary>
        public const string EmailClaimType = "email";

        /// <summary>
        /// The value associated with the authenticated user's assigned role(s).
        /// </summary>
        public const string RoleClaimType = "role";

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUser"/> class with the claims principal associated with the user and configures the properties based on the claims.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal associated with the user.</param>
        public AuthenticatedUser(ClaimsPrincipal claimsPrincipal)
        {
            this.ClaimsPrincipal = claimsPrincipal;

            this.Subject = claimsPrincipal?.Claims.SingleOrDefault(c => c.Type == SubjectClaimType)?.Value;
            this.Email = claimsPrincipal?.Claims.SingleOrDefault(c => c.Type == EmailClaimType)?.Value;
            this.Roles = claimsPrincipal?.Claims.Where(x => x.Type == RoleClaimType).Select(x => x.Value);
            this.Claims = claimsPrincipal?.Claims.ToImmutableList();
        }

        /// <summary>
        /// Gets the claims principal associated with the user.
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal { get; }

        /// <summary>
        /// Gets the authenticated user's identity.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Gets the authenticated user's preferred email address.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the collection of the authenticated user's assigned roles.
        /// </summary>
        public IEnumerable<string> Roles { get; }

        /// <summary>
        /// Gets the collection of the authenticated user's claims.
        /// </summary>
        public IImmutableList<Claim> Claims { get; }
    }
}