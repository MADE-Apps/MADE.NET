// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using MADE.Web.Extensions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines a default exception handler for exceptions thrown which are not explicitly handled.
    /// </summary>
    public class DefaultExceptionHandler : IHttpContextExceptionHandler<Exception>
    {
        /// <summary>
        /// Handles the specified exception for the given context.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <param name="exception">The exception thrown.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            var response = new ExceptionResponse<Exception>("UnhandledException", "An unhandled exception occurred.", exception);
            await context.Response.WriteJsonAsync(HttpStatusCode.InternalServerError, response);
        }
    }
}