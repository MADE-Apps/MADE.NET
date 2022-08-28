// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Identity
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines a collection of extensions for <see cref="AuthenticatedUser"/> objects.
    /// </summary>
    public static class AuthenticatedUserExtensions
    {
        /// <summary>
        /// Adds the <see cref="HttpContextAccessor"/> and <see cref="AuthenticatedUserAccessor"/> to the specified services collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The configured service collection.</returns>
        public static IServiceCollection AddAuthenticatedUserAccessor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddScoped<IAuthenticatedUserAccessor, AuthenticatedUserAccessor>();
            return serviceCollection;
        }
    }
}