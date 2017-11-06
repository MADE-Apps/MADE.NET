// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericValidationRuleSetting.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a set of enum values for determining the setting of a numeric validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    /// <summary>
    /// Defines a set of enum values for determining the setting of a numeric validation rule.
    /// </summary>
    public enum NumericValidationRuleSetting
    {
        /// <summary>
        /// No setting will be applied, any value can be valid.
        /// </summary>
        None,

        /// <summary>
        /// Any value that is positive or 0 can be valid.
        /// </summary>
        Positive,

        /// <summary>
        /// Any value that is negative (less than 0) can be valid.
        /// </summary>
        Negative
    }
}