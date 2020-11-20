// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value contains alpha characters.
    /// </summary>
    public class AlphaValidator : RegexValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaValidator"/> class with the expected RegEx pattern.
        /// </summary>
        public AlphaValidator()
        {
            this.Key = nameof(AlphaValidator);
            this.Pattern = "^[a-zA-Z]*$";
        }

        /// <summary>
        /// Gets or sets the feedback message to display when <see cref="IValidator.IsInvalid"/> is true.
        /// </summary>
        public override string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? Resources.AlphaValidator_FeedbackMessage : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }
    }
}