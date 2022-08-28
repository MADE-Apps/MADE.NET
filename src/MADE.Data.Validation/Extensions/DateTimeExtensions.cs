// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of data validation extensions for <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines whether a <see cref="DateTime"/> is within a valid range.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check.</param>
        /// <param name="from">The lower bound of the range.</param>
        /// <param name="to">The upper bound of the range.</param>
        /// <returns>True if the date is within the valid range.</returns>
        public static bool IsInRange(this DateTime date, DateTime from, DateTime to)
        {
            return date >= from && date <= to;
        }

        /// <summary>
        /// Determines whether a <see cref="DateTime"/> is a day of the week other than Sunday or Saturday.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check.</param>
        /// <returns>True if the day of week is between Monday and Friday; otherwise, false.</returns>
        public static bool IsWeekday(this DateTime date)
        {
            return date.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday;
        }

        /// <summary>
        /// Determines whether a <see cref="DateTime"/> is a day of the week other than Monday through Friday.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check.</param>
        /// <returns>True if the day of week is Saturday or Sunday; otherwise, false.</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
        }
    }
}