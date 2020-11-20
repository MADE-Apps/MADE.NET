// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Validators
{
    using System.Collections;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is provided.
    /// </summary>
    public class RequiredValidator : IValidator
    {
        private string feedbackMessage = Resources.ResourceManager.GetString("RequiredValidator_FeedbackMessage");

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(RequiredValidator);

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
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? Resources.RequiredValidator_FeedbackMessage : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            this.IsInvalid = DetermineIsInvalid(value);
            this.IsDirty = true;
        }

        private static bool DetermineIsInvalid(object value)
        {
            switch (value)
            {
                case null:
                    return true;
                case ICollection collection:
                    return collection.Count <= 0;
                case bool isTrue:
                    return !isTrue;
                case string str:
                    return str.IsNullOrWhiteSpace();
                default:
                    return false;
            }
        }
    }
}