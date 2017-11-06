// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationRules.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a container for <see cref="IValidationRule" /> objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a container for <see cref="IValidationRule"/> objects.
    /// </summary>
    public class ValidationRules : IValidationRules
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRules"/> class.
        /// </summary>
        public ValidationRules()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRules"/> class.
        /// </summary>
        /// <param name="rules">
        /// A predefined list of rules to populate with.
        /// </param>
        public ValidationRules(IEnumerable<IValidationRule> rules)
        {
            this.Rules = new List<IValidationRule>();

            if (rules != null)
            {
                this.Rules.AddRange(rules);
            }
        }

        /// <summary>
        /// Gets or sets the current error message to display. 
        /// This property is set by calling the <see cref="IsValid"/> method.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets the collection of registered data validation rules.
        /// </summary>
        public List<IValidationRule> Rules { get; }

        /// <summary>
        /// Checks whether the given value is valid according to the defined <see cref="Rules"/> collection.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the value is valid.
        /// </returns>
        public bool IsValid(object value)
        {
            bool[] isInvalid = { false };

            foreach (IValidationRule rule in this.Rules.TakeWhile(rule => !isInvalid[0]))
            {
                isInvalid[0] = !rule.IsValid(value);

                if (isInvalid[0])
                {
                    this.ErrorMessage = rule.ErrorMessage;
                }
            }

            return !isInvalid[0];
        }
    }
}