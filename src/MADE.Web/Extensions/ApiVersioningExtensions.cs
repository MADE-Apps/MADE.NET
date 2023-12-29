// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Extensions
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines a collection of extensions for API versioning.
    /// </summary>
    public static class ApiVersioningExtensions
    {
        /// <summary>
        /// Adds request API versioning for controllers and APIs to the specified services collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection">services</see> available in the application.</param>
        /// <param name="defaultMajor">The default major version of the API. Default, 1.</param>
        /// <param name="defaultMinor">The default minor version of the API. Default, 0.</param>
        /// <returns>The configured <paramref name="services"/> object.</returns>
        public static IServiceCollection AddApiVersionSupport(
            this IServiceCollection services,
            int defaultMajor = 1,
            int defaultMinor = 0)
        {
            var apiVersioningBuilder = services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(defaultMajor, defaultMinor);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            apiVersioningBuilder.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            return services;
        }

        /// <summary>
        /// Adds request header API versioning for controllers and APIs to the specified services collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection">services</see> available in the application.</param>
        /// <param name="apiHeaderName">The name of the header that is required when making requests to API endpoints. Default, x-api-version.</param>
        /// <param name="defaultMajor">The default major version of the API. Default, 1.</param>
        /// <param name="defaultMinor">The default minor version of the API. Default, 0.</param>
        /// <returns>The configured <paramref name="services"/> object.</returns>
        public static IServiceCollection AddApiVersionHeaderSupport(
            this IServiceCollection services,
            string apiHeaderName = "x-api-version",
            int defaultMajor = 1,
            int defaultMinor = 0)
        {
            var apiVersioningBuilder = services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(defaultMajor, defaultMinor);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader(apiHeaderName);
            });

            apiVersioningBuilder.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            return services;
        }
    }
}