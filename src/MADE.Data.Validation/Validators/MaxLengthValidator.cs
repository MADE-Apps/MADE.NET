namespace MADE.Data.Validation.Validators
{
    using System;
    using System.Collections;
    using MADE.Data.Validation;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is less than a maximum length.
    /// </summary>
    public class MaxLengthValidator : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxLengthValidator"/> class.
        /// </summary>
        public MaxLengthValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxLengthValidator"/> class with a maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        public MaxLengthValidator(IComparable max)
        {
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(MaxLengthValidator);

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
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? string.Format(Resources.MaxLengthValidator_FeedbackMessage, this.Max) : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public IComparable Max { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid = value switch
            {
                string str => str.Length.IsGreaterThan(this.Max),
                ICollection col => col.Count.IsGreaterThan(this.Max),
                _ => true
            };

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }
    }
}