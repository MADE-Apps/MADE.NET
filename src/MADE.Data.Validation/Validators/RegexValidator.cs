// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System.Text.RegularExpressions;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a generic regular expression data validator.
    /// </summary>
    public class RegexValidator : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(RegexValidator);

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
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? Resources.RegexValidator_FeedbackMessage : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Gets or sets the RegEx pattern to match on.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="RegexMatchTimeoutException">Thrown if a Regex time-out occurred.</exception>
        public virtual void Validate(object value)
        {
            string str = value?.ToString() ?? string.Empty;
            this.IsInvalid = !Regex.IsMatch(str, this.Pattern);
            this.IsDirty = true;
        }
    }
}
