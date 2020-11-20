// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value contains alphanumeric characters.
    /// </summary>
    public class AlphaNumericValidator : RegexValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaNumericValidator"/> class with the expected RegEx pattern.
        /// </summary>
        public AlphaNumericValidator()
        {
            this.Key = nameof(AlphaNumericValidator);
            this.Pattern = "^[a-zA-Z0-9]*$";
        }

        /// <summary>
        /// Gets or sets the feedback message to display when <see cref="IValidator.IsInvalid"/> is true.
        /// </summary>
        public override string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? Resources.AlphaNumericValidator_FeedbackMessage : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }
    }
}
