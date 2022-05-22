// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is within the valid range for a longitude value.
    /// </summary>
    public class LongitudeValidator : IValidator
    {
        /// <summary>
        /// The minimum valid longitude value.
        /// </summary>
        public const double Min = -180;

        /// <summary>
        /// The maximum valid longitude value.
        /// </summary>
        public const double Max = 180;

        private string feedbackMessage;

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public virtual string Key { get; set; } = nameof(LongitudeValidator);

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
                ? string.Format(Resources.BetweenValidator_FeedbackMessage, Min, Max)
                : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool parsed = double.TryParse(value?.ToString() ?? string.Empty, out double longitude);
            this.IsInvalid = !parsed || longitude is < Min or > Max;
            this.IsDirty = true;
        }
    }
}