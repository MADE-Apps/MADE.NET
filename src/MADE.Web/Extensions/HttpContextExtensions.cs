// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Extensions
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines a collection of extensions for a <see cref="HttpContext" /> object.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the domain name of the requesting context.
        /// </summary>
        /// <param name="context">The requesting <see cref="HttpContext"/>.</param>
        /// <returns>The domain part of the request's host.</returns>
        public static string GetDomain(this HttpContext context)
        {
            return context.Request.Host.Host;
        }
    }
}
