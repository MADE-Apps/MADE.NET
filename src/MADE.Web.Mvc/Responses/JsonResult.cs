namespace MADE.Web.Mvc.Responses
{
    using System;
    using System.Net;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using MADE.Web.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a model for a result of a request that is serialized as JSON using Json.NET.
    /// </summary>
    public class JsonResult : ActionResult, IStatusCodeActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResult"/> class with the object to serialize.
        /// </summary>
        /// <param name="value">The value object to serialize.</param>
        /// <param name="statusCode">The expected result HTTP status code.</param>
        /// <param name="serializerSettings">The Json.Net serializer settings for serializing the result.</param>
        public JsonResult(
            object value,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            JsonSerializerSettings serializerSettings = default)
        {
            this.Value = value;
            this.StatusCode = (int)statusCode;
            this.SerializerSettings = serializerSettings;
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
        /// Gets the Json.Net serializer settings for serializing the result.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; }

        /// <summary>
        /// Executes the result operation of the action method asynchronously writing the <see cref="Value"/> to the response.
        /// </summary>
        /// <param name="context">The context in which the result is executed.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="context"/> is <see langword="null"/>.</exception>
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpResponse response = context.HttpContext.Response;

            ExceptionDispatchInfo exceptionDispatchInfo = null;
            try
            {
                await response.WriteJsonAsync(
                    this.StatusCode.GetValueOrDefault((int)HttpStatusCode.OK),
                    this.Value,
                    this.SerializerSettings);
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
}