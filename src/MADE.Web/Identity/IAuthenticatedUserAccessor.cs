// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Security.Claims;

namespace MADE.Web.Identity;

/// <summary>
/// Defines an interface for accessing an authenticated user's claims principal.
/// </summary>
public interface IAuthenticatedUserAccessor
{
    /// <summary>
    /// Gets the authenticated user's claims principal.
    /// </summary>
    ClaimsPrincipal? ClaimsPrincipal { get; }

    /// <summary>
    /// Gets the authenticated user model for the specified <see cref="ClaimsPrincipal"/>/
    /// </summary>
    AuthenticatedUser AuthenticatedUser { get; }
}
