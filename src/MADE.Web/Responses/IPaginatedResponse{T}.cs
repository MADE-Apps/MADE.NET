// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Responses
{
    using System.Collections.Generic;
    using MADE.Web.Requests;

    /// <summary>
    /// Defines an interface for a response to a <see cref="IPaginatedRequest{T}"/> request.
    /// </summary>
    /// <typeparam name="T">The type of item to return.</typeparam>
    public interface IPaginatedResponse<T>
    {
        /// <summary>
        /// Gets or sets the items associated with the page.
        /// </summary>
        IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the page associated with the results.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of expected results for the page.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the count of the number of available items.
        /// </summary>
        int AvailableCount { get; set; }

        /// <summary>
        /// Gets the total number of pages for the available items based on the page size.
        /// </summary>
        int TotalPages { get; }
    }
}