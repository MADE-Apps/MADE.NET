namespace MADE.UI.Controls
{
    using MADE.Data.Validation;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines an interface for a component which validates an input.
    /// </summary>
    public interface IInputValidator
    {
        /// <summary>
        /// Occurs when the input value is validated against the collection of validators.
        /// </summary>
        event InputValidatedEventHandler Validated;

        /// <summary>
        /// Gets or sets the style associated with the feedback message.
        /// </summary>
        Style FeedbackMessageStyle { get; set; }

        /// <summary>
        /// Gets or sets the input to run validation against.
        /// </summary>
        public object Input { get; set; }

        /// <summary>
        /// Gets or sets the validators to run on the input.
        /// </summary>
        ValidatorCollection Validators { get; set; }
    }
}
