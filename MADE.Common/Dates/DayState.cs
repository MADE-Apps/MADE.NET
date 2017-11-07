// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DayState.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines enum values for the states of a day.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Common.Dates
{
    /// <summary>
    /// Defines enum values for the states of a day.
    /// </summary>
    public enum DayState
    {
        /// <summary>
        /// The default state, unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The morning, usually 5AM - 12PM.
        /// </summary>
        Morning,

        /// <summary>
        /// The afternoon, usually 12PM - 5PM.
        /// </summary>
        Afternoon,

        /// <summary>
        /// The evening, usually 5PM - 8PM.
        /// </summary>
        Evening,

        /// <summary>
        /// The night, usually 8PM - 5AM.
        /// </summary>
        Night
    }
}