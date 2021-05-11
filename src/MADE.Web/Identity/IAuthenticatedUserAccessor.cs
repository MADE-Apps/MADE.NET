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
    }
}