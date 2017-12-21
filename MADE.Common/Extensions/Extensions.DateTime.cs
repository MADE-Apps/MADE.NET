// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.DateTime.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of common extensions for DateTime objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Common
{
    using System;
    using System.Globalization;

    using MADE.Common.Dates;

    /// <summary>
    /// Defines a collection of common extensions for <see cref="DateTime"/> objects.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Retrieves the <see cref="DateTime"/> value associated with the starting date of the week depending on the current culture for the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get the first date of the week for.
        /// </param>
        /// <returns>
        /// Returns a <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetFirstDateOfWeek(this DateTime dateTime)
        {
            return GetFirstDateOfWeek(dateTime, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Retrieves the <see cref="DateTime"/> value associated with the starting date of the week depending on the given culture for the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get the first date of the week for.
        /// </param>
        /// <param name="culture">
        /// The culture information to use.
        /// </param>
        /// <returns>
        /// Returns a <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetFirstDateOfWeek(this DateTime dateTime, CultureInfo culture)
        {
            if (culture == null)
            {
                // If no culture is specified, we will use the current.
                culture = CultureInfo.CurrentCulture;
            }

            DateTimeFormatInfo format = culture.DateTimeFormat;
            DayOfWeek expectedFirstDay = format.FirstDayOfWeek;
            int dayDifference = dateTime.DayOfWeek - expectedFirstDay;

            if (dayDifference < 0)
            {
                dayDifference += 7;
            }

            return dateTime.AddDays(-dayDifference);

        }

        /// <summary>
        /// Gets the current age in years for the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get an age for.
        /// </param>
        /// <returns>
        /// Returns the age as a <see cref="int"/>.
        /// </returns>
        public static int GetCurrentAgeInYears(this DateTime dateTime)
        {
            int ageInYears = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(ageInYears))
            {
                ageInYears--;
            }

            return ageInYears;
        }

        /// <summary>
        /// Gets the current state of the day for the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get the state of.
        /// </param>
        /// <returns>
        /// Returns the <see cref="DayState"/>.
        /// </returns>
        public static DayState GetDayState(this DateTime dateTime)
        {
            int hours = dateTime.Hour;

            if (hours >= 5 && hours < 12)
            {
                return DayState.Morning;
            }

            if (hours >= 12 && hours < 17)
            {
                return DayState.Afternoon;
            }

            if (hours >= 17 && hours < 20)
            {
                return DayState.Evening;
            }

            return DayState.Night;
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is less than the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is less than.
        /// </param>
        /// <param name="minDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is less than <paramref name="minDate"/>.
        /// </returns>
        public static bool IsLessThan(this DateTime dateTime, DateTime minDate)
        {
            return IsLessThan(dateTime, minDate, true);
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is less than the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is less than.
        /// </param>
        /// <param name="minDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <param name="includeTime">
        /// A value indicating whether to include the time component.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is less than <paramref name="minDate"/>.
        /// </returns>
        public static bool IsLessThan(this DateTime dateTime, DateTime minDate, bool includeTime)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = minDate.AddSeconds(-1 * minDate.Second);

            if (dateTime1.Date < dateTime2.Date)
            {
                return true;
            }

            if (!includeTime)
            {
                return false;
            }

            if (dateTime1.Date != dateTime2.Date)
            {
                return false;
            }

            TimeSpan timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
            TimeSpan timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

            return timeSpan1 < timeSpan2;
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is greater than the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is greater than.
        /// </param>
        /// <param name="maxDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is greater than <paramref name="maxDate"/>.
        /// </returns>
        public static bool IsGreaterThan(this DateTime dateTime, DateTime maxDate)
        {
            return IsGreaterThan(dateTime, maxDate, true);
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is greater than the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is greater than.
        /// </param>
        /// <param name="maxDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <param name="includeTime">
        /// A value indicating whether to include the time component.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is greater than <paramref name="maxDate"/>.
        /// </returns>
        public static bool IsGreaterThan(this DateTime dateTime, DateTime maxDate, bool includeTime)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = maxDate.AddSeconds(-1 * maxDate.Second);

            if (dateTime1.Date > dateTime2.Date)
            {
                return true;
            }

            if (!includeTime)
            {
                return false;
            }

            if (dateTime1.Date != dateTime2.Date)
            {
                return false;
            }

            TimeSpan timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
            TimeSpan timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

            return timeSpan1 > timeSpan2;
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is less than or equal to the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is less than or equal to.
        /// </param>
        /// <param name="minDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is less than or equal to <paramref name="minDate"/>.
        /// </returns>
        public static bool IsLessThanOrEqualTo(this DateTime dateTime, DateTime minDate)
        {
            return IsLessThanOrEqualTo(dateTime, minDate, true);
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is less than or equal to the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is less than or equal to.
        /// </param>
        /// <param name="minDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <param name="includeTime">
        /// A value indicating whether to include the time component.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is less than or equal to <paramref name="minDate"/>.
        /// </returns>
        public static bool IsLessThanOrEqualTo(this DateTime dateTime, DateTime minDate, bool includeTime)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = minDate.AddSeconds(-1 * minDate.Second);

            if (dateTime1.Date <= dateTime2.Date)
            {
                return true;
            }

            if (!includeTime)
            {
                return false;
            }

            if (dateTime1.Date != dateTime2.Date)
            {
                return false;
            }

            TimeSpan timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
            TimeSpan timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

            return timeSpan1 <= timeSpan2;
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is greater than or equal to the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is greater than or equal to.
        /// </param>
        /// <param name="maxDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is greater than or equal to <paramref name="maxDate"/>.
        /// </returns>
        public static bool IsGreaterThanOrEqualTo(this DateTime dateTime, DateTime maxDate)
        {
            return IsGreaterThanOrEqualTo(dateTime, maxDate, true);
        }

        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is greater than or equal to the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is greater than or equal to.
        /// </param>
        /// <param name="maxDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <param name="includeTime">
        /// A value indicating whether to include the time component.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is greater than or equal to <paramref name="maxDate"/>.
        /// </returns>
        public static bool IsGreaterThanOrEqualTo(this DateTime dateTime, DateTime maxDate, bool includeTime)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = maxDate.AddSeconds(-1 * maxDate.Second);

            if (dateTime1.Date >= dateTime2.Date)
            {
                return true;
            }

            if (!includeTime)
            {
                return false;
            }

            if (dateTime1.Date != dateTime2.Date)
            {
                return false;
            }

            TimeSpan timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
            TimeSpan timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

            return timeSpan1 >= timeSpan2;
        }
    }
}