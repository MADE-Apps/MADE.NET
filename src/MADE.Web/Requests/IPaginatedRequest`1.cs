// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Requests
{
    /// <summary>
    /// Defines an interface for a request with paginated results of the specified response type.
    /// </summary>
    /// <typeparam name="T">The type of item to return.</typeparam>
    public interface IPaginatedRequest<T>
    {
        /// <summary>
        /// Gets or sets the page requested.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of expected results for the requested page.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets the number of items to skip.
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// Gets the number of items to take.
        /// </summary>
        int Take { get; }
    }
}
