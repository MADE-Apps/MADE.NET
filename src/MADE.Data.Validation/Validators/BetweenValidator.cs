// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is within a minimum and maximum range.
    /// </summary>
    public class BetweenValidator : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenValidator"/> class.
        /// </summary>
        public BetweenValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenValidator"/> class with a minimum and maximum range.
        /// </summary>
        /// <param name="min">The minimum value within the range.</param>
        /// <param name="max">The maximum value within the range.</param>
        public BetweenValidator(IComparable min, IComparable max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(BetweenValidator);

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
            get => this.feedbackMessage.IsNullOrWhiteSpace()
                ? string.Format(Resources.BetweenValidator_FeedbackMessage, this.Min, this.Max)
                : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Gets or sets the minimum value within the range.
        /// </summary>
        public IComparable Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum value within the range.
        /// </summary>
        public IComparable Max { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the range is inclusive.
        /// </summary>
        /// <remarks>
        /// By default, the value is <c>true</c>.
        /// </remarks>
        public bool Inclusive { get; set; } = true;

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid = true;

            if (value is IComparable comparable)
            {
                if (this.Inclusive)
                {
                    isInvalid = comparable.IsLessThan(this.Min) || comparable.IsGreaterThan(this.Max);
                }
                else
                {
                    isInvalid = comparable.IsLessThanOrEqualTo(this.Min) || comparable.IsGreaterThanOrEqualTo(this.Max);
                }
            }

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }
    }
}