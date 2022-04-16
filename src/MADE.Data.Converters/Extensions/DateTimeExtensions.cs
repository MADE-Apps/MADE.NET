// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for a date/time object.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the day suffix for the specified date, i.e. st, nd, rd, or th.
        /// </summary>
        /// <param name="dateTime">The date to get a day suffix for.</param>
        /// <returns>The day suffix as a string.</returns>
        public static string ToDaySuffix(this DateTime dateTime)
        {
            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        /// <summary>
        /// Gets the current age in years based on the specified starting date and today's date.
        /// </summary>
        /// <param name="startingDate">
        /// The starting date.
        /// </param>
        /// <returns>
        /// An integer value representing the number of years.
        /// </returns>
        public static int ToCurrentAge(this DateTime startingDate)
        {
            int yearDifference = DateTime.Now.Year - startingDate.Year;
            if (DateTime.Now < startingDate.AddYears(yearDifference))
            {
                yearDifference--;
            }

            return yearDifference;
        }

        /// <summary>
        /// Rounds a <see cref="DateTime"/> value to its nearest hour.
        /// <para>
        /// This is determined by the half hour of each hour, rounding up or down.
        /// </para>
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to round.</param>
        /// <returns>The updated <see cref="DateTime"/>.</returns>
        public static DateTime ToNearestHour(this DateTime dateTime)
        {
            int hour = dateTime.Minute < 30
                ? dateTime.Hour
                : dateTime.Hour + 1;

            return hour == 24
                ? new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0).AddDays(1)
                : new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, 0, 0);
        }

        /// <summary>
        /// Gets the start of the day represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the same date as this instance, and the time value set to midnight.</returns>
        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the end of the day represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the same date as this instance, and the time value set to just before midnight of the next day.</returns>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.StartOfDay().AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Gets the first day of the week represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the first day of the week for this instance, and the time value set to midnight.</returns>
        public static DateTime StartOfWeek(this DateTime dateTime)
        {
            return dateTime.AddDays(-(int)dateTime.DayOfWeek).StartOfDay();
        }

        /// <summary>
        /// Gets the last day of the week represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the last day of the week for this instance, and the time value set to just before midnight of the next day.</returns>
        public static DateTime EndOfWeek(this DateTime dateTime)
        {
            return dateTime.StartOfWeek().AddDays(7).EndOfDay();
        }

        /// <summary>
        /// Gets the first day of the month represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the first day of the month for this instance, and the time value set to midnight.</returns>
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Gets the last day of the month represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the last day of the month for this instance, and the time value set to just before midnight of the next day.</returns>
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            return dateTime.StartOfMonth().AddMonths(1).AddDays(-1).EndOfDay();
        }

        /// <summary>
        /// Gets the first day of the year represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the first day of the year for this instance, and the time value set to midnight.</returns>
        public static DateTime StartOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// Gets the last day of the year represented by the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/>.</param>
        /// <returns>A new object with the last day of the year for this instance, and the time value set to just before midnight of the next day.</returns>
        public static DateTime EndOfYear(this DateTime dateTime)
        {
            return dateTime.StartOfYear().AddYears(1).AddDays(-1).EndOfDay();
        }

        /// <summary>
        /// Sets the time value of a nullable date/time value.
        /// </summary>
        /// <param name="dateTime">The nullable date/time value to add a time to.</param>
        /// <param name="timeSpan">The time to set on the date/time value.</param>
        /// <returns>The updated date/time with the given time value.</returns>
        public static DateTime? SetTime(this DateTime? dateTime, TimeSpan timeSpan)
        {
            return SetTime(dateTime, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        /// <summary>
        /// Sets the time value of a nullable date/time value.
        /// </summary>
        /// <param name="dateTime">
        /// The nullable date/time value to add a time to.
        /// </param>
        /// <param name="hours">
        /// The hours to set on the date/time value.
        /// </param>
        /// <param name="minutes">
        /// The minutes to set on the date/time value.
        /// </param>
        /// <returns>
        /// The updated date/time with the given time value.
        /// </returns>
        public static DateTime? SetTime(this DateTime? dateTime, int hours, int minutes)
        {
            return SetTime(dateTime, hours, minutes, 0, 0);
        }

        /// <summary>
        /// Sets the time value of a nullable date/time value.
        /// </summary>
        /// <param name="dateTime">
        /// The nullable date/time value to add a time to.
        /// </param>
        /// <param name="hours">
        /// The hours to set on the date/time value.
        /// </param>
        /// <param name="minutes">
        /// The minutes to set on the date/time value.
        /// </param>
        /// <param name="seconds">
        /// The seconds to set on the date/time value.
        /// </param>
        /// <returns>
        /// The updated date/time with the given time value.
        /// </returns>
        public static DateTime? SetTime(this DateTime? dateTime, int hours, int minutes, int seconds)
        {
            return SetTime(dateTime, hours, minutes, seconds, 0);
        }

        /// <summary>
        /// Sets the time value of a nullable date/time value.
        /// </summary>
        /// <param name="dateTime">
        /// The nullable date/time value to add a time to.
        /// </param>
        /// <param name="hours">
        /// The hours to set on the date/time value.
        /// </param>
        /// <param name="minutes">
        /// The minutes to set on the date/time value.
        /// </param>
        /// <param name="seconds">
        /// The seconds to set on the date/time value.
        /// </param>
        /// <param name="milliseconds">
        /// The milliseconds to set on the date/time value.
        /// </param>
        /// <returns>
        /// The updated date/time with the given time value.
        /// </returns>
        public static DateTime? SetTime(this DateTime? dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return dateTime == null ? null : SetTime(dateTime.Value, hours, minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Sets the time value of a date/time value.
        /// </summary>
        /// <param name="dateTime">The date/time value to add a time to.</param>
        /// <param name="timeSpan">The time to set on the date/time value.</param>
        /// <returns>The updated date/time with the given time value.</returns>
        public static DateTime SetTime(this DateTime dateTime, TimeSpan timeSpan)
        {
            return SetTime(dateTime, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        /// <summary>
        /// Sets the time value of a date/time value.
        /// </summary>
        /// <param name="dateTime">
        /// The date/time value to add a time to.
        /// </param>
        /// <param name="hours">
        /// The hours to set on the date/time value.
        /// </param>
        /// <param name="minutes">
        /// The minutes to set on the date/time value.
        /// </param>
        /// <returns>
        /// The updated date/time with the given time value.
        /// </returns>
        public static DateTime SetTime(this DateTime dateTime, int hours, int minutes)
        {
            return SetTime(dateTime, hours, minutes, 0, 0);
        }

        /// <summary>
        /// Sets the time value of a date/time value.
        /// </summary>
        /// <param name="dateTime">
        /// The date/time value to add a time to.
        /// </param>
        /// <param name="hours">
        /// The hours to set on the date/time value.
        /// </param>
        /// <param name="minutes">
        /// The minutes to set on the date/time value.
        /// </param>
        /// <param name="seconds">
        /// The seconds to set on the date/time value.
        /// </param>
        /// <returns>
        /// The updated date/time with the given time value.
        /// </returns>
        public static DateTime SetTime(this DateTime dateTime, int hours, int minutes, int seconds)
        {
            return SetTime(dateTime, hours, minutes, seconds, 0);
        }

        /// <summary>
        /// Sets the time value of a date/time value.
        /// </summary>
        /// <param name="dateTime">
        /// The date/time value to add a time to.
        /// </param>
        /// <param name="hours">
        /// The hours to set on the date/time value.
        /// </param>
        /// <param name="minutes">
        /// The minutes to set on the date/time value.
        /// </param>
        /// <param name="seconds">
        /// The seconds to set on the date/time value.
        /// </param>
        /// <param name="milliseconds">
        /// The milliseconds to set on the date/time value.
        /// </param>
        /// <returns>
        /// The updated date/time with the given time value.
        /// </returns>
        public static DateTime SetTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
    }
}