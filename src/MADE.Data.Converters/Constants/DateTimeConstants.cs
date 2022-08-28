// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Constants
{
    using System;

    /// <summary>
    /// Defines a collection of constants for <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeConstants
    {
        /// <summary>
        /// Defines the minimum value for a <see cref="DateTime"/> object determined by Unix.
        /// </summary>
        public static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Defines the time at the end of a day.
        /// </summary>
        public static readonly TimeSpan EndOfDayTime = new TimeSpan(1, 0, 0, 0).Subtract(TimeSpan.FromTicks(1));
    }
}
