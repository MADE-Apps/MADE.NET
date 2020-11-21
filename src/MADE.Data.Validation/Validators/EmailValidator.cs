// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using Extensions;
    using Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is an email address.
    /// </summary>
    public class EmailValidator : RegexValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailValidator"/> class.
        /// </summary>
        public EmailValidator()
        {
            this.Key = nameof(EmailValidator);
            this.Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        }

        /// <summary>
        /// Gets or sets the feedback message to display when <see cref="IValidator.IsInvalid"/> is true.
        /// </summary>
        public override string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? Resources.EmailValidator_FeedbackMessage : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }
    }
}
