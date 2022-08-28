// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Identity
{
    using System.Security.Claims;

    /// <summary>
    /// Defines an interface for accessing an authenticated user's claims principal.
    /// </summary>
    public interface IAuthenticatedUserAccessor
    {
        /// <summary>
        /// Gets the authenticated user's claims principal.
        /// </summary>
        ClaimsPrincipal ClaimsPrincipal { get; }

        /// <summary>
        /// Gets the authenticated user model for the specified <see cref="ClaimsPrincipal"/>/
        /// </summary>
        AuthenticatedUser AuthenticatedUser { get; }
    }
}