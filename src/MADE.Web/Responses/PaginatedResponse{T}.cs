// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Responses
{
    using System.Collections.Generic;
    using MADE.Web.Requests;

    /// <summary>
    /// Defines a response to a <see cref="IPaginatedRequest{T}"/> request.
    /// </summary>
    /// <typeparam name="T">The type of item to return.</typeparam>
    public class PaginatedResponse<T> : IPaginatedResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedResponse{T}"/> class.
        /// </summary>
        /// <param name="items">The items associated with the page.</param>
        /// <param name="page">The page associated with the results.</param>
        /// <param name="pageSize">The number of expected results for the page.</param>
        /// <param name="availableCount">The count of the number of available items.</param>
        public PaginatedResponse(IEnumerable<T> items, int page, int pageSize, int availableCount)
        {
            this.Items = items;
            this.Page = page;
            this.PageSize = pageSize;
            this.AvailableCount = availableCount;
        }

        /// <summary>
        /// Gets or sets the items associated with the page.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the page associated with the results.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of expected results for the page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the count of the number of available items.
        /// </summary>
        public int AvailableCount { get; set; }

        /// <summary>
        /// Gets the total number of pages for the available items based on the page size.
        /// </summary>
        public int TotalPages =>
            this.AvailableCount == 0 || this.PageSize == 0 ? 0 : this.AvailableCount / this.PageSize;
    }
}