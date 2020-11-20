// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is less than a maximum value.
    /// </summary>
    public class MaxValueValidator : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxValueValidator"/> class.
        /// </summary>
        public MaxValueValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxValueValidator"/> class with a maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        public MaxValueValidator(IComparable max)
        {
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(MaxValueValidator);

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
        public string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? string.Format(Resources.MaxValueValidator_FeedbackMessage, this.Max) : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public IComparable Max { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid = true;

            if (value is IComparable comparable)
            {
                isInvalid = comparable.IsGreaterThan(this.Max);
            }

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }
    }
}
