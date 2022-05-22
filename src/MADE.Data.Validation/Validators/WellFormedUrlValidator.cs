// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is a valid well formed URL, e.g. https://www.example.com.
    /// </summary>
    public class WellFormedUrlValidator : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(WellFormedUrlValidator);

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
                ? Resources.UrlValidator_FeedbackMessage
                : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid;

            if (value is Uri uri)
            {
                isInvalid = uri.IsWellFormedOriginalString() == false;
            }
            else
            {
                var stringValue = value?.ToString() ?? string.Empty;
                isInvalid = !Uri.IsWellFormedUriString(stringValue, UriKind.Absolute);
            }

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }
    }
}