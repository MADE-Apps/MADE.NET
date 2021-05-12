namespace MADE.Data.Converters.Constants
{
    using System;

    /// <summary>
    /// Defines a collection of constants for <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeConstants
    {
        /// <summary>
        /// Defines the time at the end of a day.
        /// </summary>
        public static readonly TimeSpan EndOfDayTime = new TimeSpan(1, 0, 0, 0).Subtract(TimeSpan.FromTicks(1));
    }
}
