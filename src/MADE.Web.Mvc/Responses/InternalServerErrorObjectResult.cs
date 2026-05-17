// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MADE.Web.Mvc.Responses;

/// <summary>
/// Defines an <see cref="ObjectResult"/> that when executed will produce a Internal Server Error (500) response.
/// </summary>
public class InternalServerErrorObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorObjectResult"/> class.
    /// </summary>
    /// <param name="error">Contains the errors to be returned to the client.</param>
    public InternalServerErrorObjectResult(object error)
        : base(error)
    {
        this.StatusCode = DefaultStatusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorObjectResult"/> class.
    /// </summary>
    /// <param name="modelState">The <see cref="ModelStateDictionary"/> containing the validation errors.</param>
    /// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="modelState"/> is <see langword="null"/>.</exception>
    public InternalServerErrorObjectResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        ArgumentNullException.ThrowIfNull(modelState);

        this.StatusCode = DefaultStatusCode;
    }
}
