// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a data validation rule for checking a DateTime value falls within the range of a minimum and maximum date.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using System;
    using System.Globalization;

    using MADE.Common;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validation rule for checking a <see cref="DateTime"/> value falls within the range of a minimum and maximum date.
    /// </summary>
    public class DateTimeRangeValidationRule : ValidationRuleBase
    {
        private DateTime minDate;

        private DateTime maxDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRangeValidationRule"/> class.
        /// </summary>
        public DateTimeRangeValidationRule()
            : this(DateTime.MinValue, DateTime.MaxValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRangeValidationRule"/> class.
        /// </summary>
        /// <param name="minDate">
        /// The minimum date in the valid range.
        /// </param>
        /// <param name="maxDate">
        /// The maximum date in the valid range.
        /// </param>
        public DateTimeRangeValidationRule(DateTime minDate, DateTime maxDate)
        {
            this.minDate = minDate;
            this.maxDate = maxDate;

            this.UpdateErrorMessage();
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include the time component when checking the range.
        /// Default is true.
        /// </summary>
        public bool IncludeTimeInRange { get; set; } = true;

        /// <summary>
        /// Gets or sets the minimum date in the valid range.
        /// </summary>
        public DateTime MinDate
        {
            get => this.minDate;
            set
            {
                this.minDate = value;
                this.UpdateErrorMessage();
            }
        }

        /// <summary>
        /// Gets or sets the maximum date in the valid range.
        /// </summary>
        public DateTime MaxDate
        {
            get => this.maxDate;
            set
            {
                this.maxDate = value;
                this.UpdateErrorMessage();
            }
        }

        /// <summary>
        /// Checks whether the given value is a <see cref="DateTime"/> value between the <see cref="MinDate"/> and <see cref="MaxDate"/>.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the value is valid.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string valueString = value.ToString();
            if (string.IsNullOrWhiteSpace(valueString))
            {
                // If the value doesn't exist, we ignored validation.
                return true;
            }

            DateTime temp;
            return DateTime.TryParse(valueString, out temp) && temp.IsGreaterThanOrEqualTo(this.MinDate, this.IncludeTimeInRange)
                   && temp.IsLessThanOrEqualTo(this.MaxDate, this.IncludeTimeInRange);
        }

        private void UpdateErrorMessage()
        {
            this.ErrorMessage = string.Format(
                Resources.DateTimeRangeValidationRule_ErrorMessage,
                this.MinDate.ToString(CultureInfo.CurrentCulture),
                this.MaxDate.ToString(CultureInfo.CurrentCulture));
        }
    }
}