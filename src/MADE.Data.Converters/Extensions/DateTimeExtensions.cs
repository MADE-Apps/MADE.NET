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
            return dateTime == null ? (DateTime?)null : SetTime(dateTime.Value, hours, minutes, seconds, milliseconds);
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