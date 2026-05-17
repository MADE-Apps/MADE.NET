// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net;
using System.Runtime.ExceptionServices;
using MADE.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;

namespace MADE.Web.Mvc.Responses;

/// <summary>
/// Defines a model for a result of a request that is serialized as JSON.
/// </summary>
public class JsonResult : ActionResult, IStatusCodeActionResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonResult"/> class with the object to serialize.
    /// </summary>
    /// <param name="value">The value object to serialize.</param>
    /// <param name="statusCode">The expected result HTTP status code.</param>
    /// <param name="serializerOptions">The JSON serializer options for serializing the result.</param>
    public JsonResult(
        object value,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        JsonSerializerOptions? serializerOptions = default)
    {
        this.Value = value;
        this.StatusCode = (int)statusCode;
        this.SerializerOptions = serializerOptions;
    }

    /// <summary>
    /// Gets the value object to serialize.
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// Gets the expected result HTTP status code.
    /// </summary>
    public int? StatusCode { get; }

    /// <summary>
    /// Gets the JSON serializer options for serializing the result.
    /// </summary>
    public JsonSerializerOptions? SerializerOptions { get; }

    /// <summary>
    /// Executes the result operation of the action method asynchronously writing the <see cref="Value"/> to the response.
    /// </summary>
    /// <param name="context">The context in which the result is executed.</param>
    /// <returns>An asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="context"/> is <see langword="null"/>.</exception>
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        HttpResponse response = context.HttpContext.Response;

        ExceptionDispatchInfo? exceptionDispatchInfo = null;
        try
        {
            await response.WriteJsonAsync(
                this.StatusCode.GetValueOrDefault((int)HttpStatusCode.OK),
                this.Value,
                this.SerializerOptions).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
        }
        finally
        {
            exceptionDispatchInfo?.Throw();
        }
    }
}
