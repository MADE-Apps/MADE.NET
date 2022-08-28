// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Mvc.Extensions
{
    using System;
    using System.Net;
    using MADE.Web.Mvc.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;
    using JsonResult = MADE.Web.Mvc.Responses.JsonResult;

    /// <summary>
    /// Defines a collection of extensions for MVC <see cref="ControllerBase"/> instances.
    /// </summary>
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// Creates a <see cref="JsonResult"/> object from the specified value for a controller response.
        /// </summary>
        /// <param name="controller">The controller that is performing the response.</param>
        /// <param name="value">The value object to serialize.</param>
        /// <param name="statusCode">The expected result HTTP status code.</param>
        /// <param name="serializerSettings">The Json.NET serializer settings for serializing the result.</param>
        /// <returns>The created <see cref="JsonResult"/> for the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="controller"/> is <see langword="null"/>.</exception>
        public static IActionResult Json(
            this ControllerBase controller,
            object value,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            JsonSerializerSettings serializerSettings = null)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return new JsonResult(value, statusCode, serializerSettings);
        }

        /// <summary>
        /// Creates an <see cref="InternalServerErrorObjectResult"/> that produces a <see cref="StatusCodes.Status500InternalServerError"/> response.
        /// </summary>
        /// <param name="controller">The controller that is performing the response.</param>
        /// <param name="responseContent">An error object to be returned to the client.</param>
        /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="controller"/> is <see langword="null"/>.</exception>
        public static IActionResult InternalServerError(this ControllerBase controller, object responseContent)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return new InternalServerErrorObjectResult(responseContent);
        }

        /// <summary>
        /// Creates an <see cref="InternalServerErrorObjectResult"/> that produces a <see cref="StatusCodes.Status500InternalServerError"/> response.
        /// </summary>
        /// <param name="controller">The controller that is performing the response.</param>
        /// <param name="modelState">The <see cref="ModelStateDictionary" /> containing errors to be returned to the client.</param>
        /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="controller"/> or <paramref name="modelState"/> is <see langword="null"/>.</exception>
        public static IActionResult InternalServerError(this ControllerBase controller, ModelStateDictionary modelState)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            return new InternalServerErrorObjectResult(modelState);
        }
    }
}