namespace MADE.Data.Validation.Validators
{
    using System;
    using System.Collections;
    using MADE.Data.Validation;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a data validator for ensuring a value is greater than a minimum length.
    /// </summary>
    public class MinLengthValidator : IValidator
    {
        private string feedbackMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthValidator"/> class.
        /// </summary>
        public MinLengthValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthValidator"/> class with a minimum value.
        /// </summary>
        /// <param name="min">The maximum value.</param>
        public MinLengthValidator(IComparable min)
        {
            this.Min = min;
        }

        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        public string Key { get; set; } = nameof(MinLengthValidator);

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
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? string.Format(Resources.MinLengthValidator_FeedbackMessage, this.Min) : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public IComparable Min { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        public void Validate(object value)
        {
            bool isInvalid = value switch
            {
                string str => str.Length.IsLessThan(this.Min),
                ICollection col => col.Count.IsLessThan(this.Min),
                _ => true
            };

            this.IsInvalid = isInvalid;
            this.IsDirty = true;
        }
    }
}