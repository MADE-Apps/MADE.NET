// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MADE.Networking.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MADE.Networking.Extensions;

/// <summary>
/// Defines a collection of extensions for registering MADE.NET Networking services with dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="INetworkRequestFactory"/> and its dependencies to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddNetworkRequestFactory(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.TryAddSingleton<INetworkRequestFactory, NetworkRequestFactory>();
        return services;
    }

    /// <summary>
    /// Adds the <see cref="INetworkRequestFactory"/> and configures a named <see cref="HttpClient"/> with the specified <paramref name="configureClient"/> action.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="clientName">The name of the <see cref="HttpClient"/> to configure.</param>
    /// <param name="configureClient">An action to configure the <see cref="HttpClient"/>.</param>
    /// <returns>The <see cref="IHttpClientBuilder"/> for further configuration.</returns>
    public static IHttpClientBuilder AddNetworkRequestFactory(
        this IServiceCollection services,
        string clientName,
        Action<HttpClient> configureClient)
    {
        services.TryAddSingleton<INetworkRequestFactory, NetworkRequestFactory>();
        return services.AddHttpClient(clientName, configureClient);
    }
}
