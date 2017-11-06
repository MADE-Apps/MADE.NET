// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DecimalValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a data validation rule for checking a value can be parsed as a decimal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validation rule for checking a value can be parsed as a <see cref="decimal"/>.
    /// </summary>
    public class DecimalValidationRule : ValidationRuleBase, INumericValidationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalValidationRule"/> class.
        /// </summary>
        public DecimalValidationRule()
        {
            this.ErrorMessage = Resources.DecimalValidationRule_ErrorMessage;
        }

        /// <summary>
        /// Gets or sets the numeric validation rule setting for supporting all, only positive or only negative values as valid.
        /// </summary>
        public NumericValidationRuleSetting NumericValidationSetting { get; set; } = NumericValidationRuleSetting.None;

        /// <summary>
        /// Checks whether the given value is a valid <see cref="decimal"/> value.
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

            bool parsed = decimal.TryParse(valueString, out decimal temp);
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