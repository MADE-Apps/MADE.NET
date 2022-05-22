// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System.Text.RegularExpressions;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is a valid base64 value.
    /// </summary>
    public class Base64Validator : RegexValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Base64Validator"/> class.
        /// </summary>
        public Base64Validator()
        {
            this.Key = nameof(Base64Validator);
            this.Pattern = @"^[a-zA-Z0-9\+/]*={0,3}$";
        }

        /// <summary>
        /// Gets or sets the feedback message to display when <see cref="IValidator.IsInvalid"/> is true.
        /// </summary>
        public override string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace()
                ? Resources.Base64Validator_FeedbackMessage
                : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="RegexMatchTimeoutException">Thrown if a Regex time-out occurred.</exception>
        public override void Validate(object value)
        {
            var stringValue = value.ToString();
            if (stringValue.Length % 4 != 0)
            {
                this.IsInvalid = true;
            }
            else
            {
                base.Validate(value);
            }

            this.IsDirty = true;
        }
    }
}