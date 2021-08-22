// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Requests
{
    using MADE.Web.Extensions;

    /// <summary>
    /// Defines a request with paginated results of the specified response type.
    /// </summary>
    /// <typeparam name="T">The type of item to return.</typeparam>
    public class PaginatedRequest<T> : IPaginatedRequest<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedRequest{T}"/> class.
        /// </summary>
        /// <param name="page">The page requested.</param>
        /// <param name="pageSize">The number of expected results for the requested page.</param>
        public PaginatedRequest(int page = 1, int pageSize = 10)
        {
            this.Page = page;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the page requested.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of expected results for the requested page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the number of items to skip.
        /// </summary>
        public int Skip => (this.Page.LimitRange(1, 100000) - 1) * this.PageSize.LimitRange(1, 100);

        /// <summary>
        /// Gets the number of items to take.
        /// </summary>
        public int Take => this.PageSize;
    }
}
