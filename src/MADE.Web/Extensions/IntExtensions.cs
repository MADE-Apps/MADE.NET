// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for integer values.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Limits the range of a value within a minimum and maximum range.
        /// </summary>
        /// <param name="value">The value to limit.</param>
        /// <param name="minimum">The minimum valid value.</param>
        /// <param name="maximum">The maximum valid value.</param>
        /// <returns>The valid value within the range.</returns>
        internal static int LimitRange(this int value, int minimum, int maximum)
        {
            return Math.Min(maximum, Math.Max(minimum, value));
        }
    }
}