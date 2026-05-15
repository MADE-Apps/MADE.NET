// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net;
using System.Net.Http;

namespace MADE.Networking.Http;

/// <summary>
/// Defines a delegating handler that retries failed HTTP requests with exponential backoff.
/// </summary>
/// <remarks>
/// Use this handler when constructing an <see cref="HttpClient"/> to automatically retry transient failures.
/// <code>
/// var handler = new RetryDelegatingHandler(maxRetries: 3, initialDelay: TimeSpan.FromSeconds(1));
/// var client = new HttpClient(handler);
/// </code>
/// </remarks>
public class RetryDelegatingHandler : DelegatingHandler
{
    private static readonly HashSet<HttpStatusCode> TransientStatusCodes = new()
    {
        HttpStatusCode.RequestTimeout,
        HttpStatusCode.TooManyRequests,
        HttpStatusCode.InternalServerError,
        HttpStatusCode.BadGateway,
        HttpStatusCode.ServiceUnavailable,
        HttpStatusCode.GatewayTimeout,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
    /// </summary>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 3.</param>
    /// <param name="initialDelay">The initial delay before the first retry. Default is 1 second.</param>
    public RetryDelegatingHandler(int maxRetries = 3, TimeSpan? initialDelay = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);
        ArgumentOutOfRangeException.ThrowIfNegative(initialDelay?.TotalMilliseconds ?? 0);

        this.InnerHandler = new HttpClientHandler();
        this.MaxRetries = maxRetries;
        this.InitialDelay = initialDelay ?? TimeSpan.FromSeconds(1);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class with the specified inner handler.
    /// </summary>
    /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 3.</param>
    /// <param name="initialDelay">The initial delay before the first retry. Default is 1 second.</param>
    public RetryDelegatingHandler(HttpMessageHandler innerHandler, int maxRetries = 3, TimeSpan? initialDelay = null)
        : base(innerHandler)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxRetries);
        ArgumentOutOfRangeException.ThrowIfNegative(initialDelay?.TotalMilliseconds ?? 0);

        this.MaxRetries = maxRetries;
        this.InitialDelay = initialDelay ?? TimeSpan.FromSeconds(1);
    }

    /// <summary>
    /// Gets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetries { get; }

    /// <summary>
    /// Gets the initial delay before the first retry. Each subsequent retry doubles the delay.
    /// </summary>
    public TimeSpan InitialDelay { get; }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage? response = null;

        for (int attempt = 0; attempt <= this.MaxRetries; attempt++)
        {
            try
            {
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (!IsTransientFailure(response) || attempt == this.MaxRetries)
                {
                    return response;
                }

                response.Dispose();
            }
            catch (HttpRequestException) when (attempt < this.MaxRetries)
            {
                // Transient network error, will retry
            }
            catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested && attempt < this.MaxRetries)
            {
                // Timeout, will retry
            }

            TimeSpan delay = TimeSpan.FromMilliseconds(this.InitialDelay.TotalMilliseconds * Math.Pow(2, attempt));
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }

        return response!;
    }

    private static bool IsTransientFailure(HttpResponseMessage response)
    {
        return TransientStatusCodes.Contains(response.StatusCode);
    }
}
