// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a data validation rule for checking a value can be parsed as an integer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validation rule for checking a value can be parsed as a <see cref="int"/>.
    /// </summary>
    public class IntValidationRule : ValidationRuleBase, INumericValidationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntValidationRule"/> class.
        /// </summary>
        public IntValidationRule()
        {
            this.ErrorMessage = Resources.IntValidationRule_ErrorMessage;
        }

        /// <summary>
        /// Gets or sets the numeric validation rule setting for supporting all, only positive or only negative values as valid.
        /// </summary>
        public NumericValidationRuleSetting NumericValidationSetting { get; set; } = NumericValidationRuleSetting.None;

        /// <summary>
        /// Checks whether the given value is a valid <see cref="int"/> value.
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

            bool parsed = int.TryParse(valueString, out int temp);
            if (!parsed)
            {
                return false;
            }

            switch (this.NumericValidationSetting)
            {
                case NumericValidationRuleSetting.Positive:
                    return temp >= 0;
                case NumericValidationRuleSetting.Negative:
                    return temp <= 0;
                default:
                    return true;
            }
        }
    }
}