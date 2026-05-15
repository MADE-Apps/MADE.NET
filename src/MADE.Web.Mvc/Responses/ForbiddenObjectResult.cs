// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MADE.Web.Mvc.Responses;

/// <summary>
/// Defines an <see cref="ObjectResult"/> that when executed will produce a Forbidden (403) response.
/// </summary>
public class ForbiddenObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status403Forbidden;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenObjectResult"/> class.
    /// </summary>
    /// <param name="error">Contains the errors to be returned to the client.</param>
    public ForbiddenObjectResult(object error)
        : base(error)
    {
        this.StatusCode = DefaultStatusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenObjectResult"/> class.
    /// </summary>
    /// <param name="modelState">The <see cref="ModelStateDictionary"/> containing the validation errors.</param>
    /// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="modelState"/> is <see langword="null"/>.</exception>
    public ForbiddenObjectResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        ArgumentNullException.ThrowIfNull(modelState);

        this.StatusCode = DefaultStatusCode;
    }
}
