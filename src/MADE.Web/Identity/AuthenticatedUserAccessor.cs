// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Identity
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines an accessor for retrieving the authenticated user from a <see cref="IHttpContextAccessor"/>.
    /// </summary>
    public class AuthenticatedUserAccessor : IAuthenticatedUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserAccessor"/> class with an instance of the <see cref="IHttpContextAccessor"/>.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        public AuthenticatedUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the authenticated user's claims principal.
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal => this.httpContextAccessor?.HttpContext?.User;

        /// <summary>
        /// Gets the authenticated user model for the specified <see cref="ClaimsPrincipal"/>/
        /// </summary>
        public AuthenticatedUser AuthenticatedUser => new(this.ClaimsPrincipal);
    }
}