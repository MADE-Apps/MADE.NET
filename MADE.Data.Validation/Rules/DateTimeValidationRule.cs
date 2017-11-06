// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a data validation rule for checking a value can be parsed as a DateTime.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using System;

    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validation rule for checking a value can be parsed as a <see cref="DateTime"/>.
    /// </summary>
    public class DateTimeValidationRule : ValidationRuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeValidationRule"/> class.
        /// </summary>
        public DateTimeValidationRule()
        {
            this.ErrorMessage = Resources.DateTimeValidationRule_ErrorMessage;
        }

        /// <summary>
        /// Checks whether the given value is a valid <see cref="DateTime"/> value.
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
            return DateTime.TryParse(valueString, out temp);
        }
    }
}