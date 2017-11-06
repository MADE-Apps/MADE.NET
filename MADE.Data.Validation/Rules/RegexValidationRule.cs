// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a data validation rule for checking a value is a match to a regular expression.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines a data validation rule for checking a value is a match to a regular expression.
    /// </summary>
    public class RegexValidationRule : ValidationRuleBase
    {
        /// <summary>
        /// Gets or sets the regular expression to match against.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets the options to apply to the regex value.
        /// </summary>
        public RegexOptions RegexOptions { get; set; }

        /// <summary>
        /// Checks whether the given value is a valid match to the <see cref="Regex"/> value.
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
                return true;
            }

            Regex reg = new Regex(this.Regex, this.RegexOptions);
            return reg.IsMatch(valueString);
        }
    }
}