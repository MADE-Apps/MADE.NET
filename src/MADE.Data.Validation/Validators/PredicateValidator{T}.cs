// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;
    using System.Text.RegularExpressions;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a generic data validator that performs custom validation logic based on the value.
    /// </summary>
    /// <typeparam name="T">The type of value being validated.</typeparam>
    public class PredicateValidator<T> : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateValidator{T}"/> class.
        /// </summary>
        public PredicateValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateValidator{T}"/> class with the custom validation logic.
        /// </summary>
        /// <param name="predicate">The logic for performing validation on the value.</param>
        public PredicateValidator(Func<T, bool> predicate)
        {
            this.Predicate = predicate;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(PredicateValidator<T>);

        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the feedback message to display when <see cref="IValidator.IsInvalid"/> is true.
        /// </summary>
        public virtual string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace()
                ? Resources.PredicateValidator_FeedbackMessage
                : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Gets or sets the logic for performing validation on the value.
        /// </summary>
        public Func<T, bool> Predicate { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="RegexMatchTimeoutException">Thrown if a Regex time-out occurred.</exception>
        public void Validate(object value)
        {
            var typedValue = (T)value;
            this.IsInvalid = !this.Predicate(typedValue);
            this.IsDirty = true;
        }
    }
}