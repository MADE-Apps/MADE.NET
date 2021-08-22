// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using MADE.Data.Validation;
    using MADE.Data.Validation.Extensions;
    using MADE.UI.Extensions;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Automation.Peers;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a component which validates an input.
    /// </summary>
    [TemplatePart(Name = ValidatorFeedbackMessagePart, Type = typeof(TextBlock))]
    public partial class InputValidator : ContentControl, IInputValidator
    {
        /// <summary>
        /// Identifies the <see cref="Input"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register(
            nameof(Input),
            typeof(object),
            typeof(InputValidator),
            new PropertyMetadata(default, (o, args) => ((InputValidator)o).InvokeValidators()));

        /// <summary>
        /// Identifies the <see cref="Validators"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValidatorsProperty = DependencyProperty.Register(
            nameof(Validators),
            typeof(ValidatorCollection),
            typeof(InputValidator),
            new PropertyMetadata(default(ValidatorCollection), (o, args) => ((InputValidator)o).InvokeValidators()));

        /// <summary>
        /// Identifies the <see cref="FeedbackMessageStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FeedbackMessageStyleProperty = DependencyProperty.Register(
            nameof(FeedbackMessageStyle),
            typeof(Style),
            typeof(InputValidator),
            new PropertyMetadata(default(Style)));

        private const string ValidatorFeedbackMessagePart = "ValidatorFeedbackMessage";

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidator"/> class.
        /// </summary>
        public InputValidator()
        {
            this.DefaultStyleKey = typeof(InputValidator);
        }

        /// <summary>
        /// Occurs when the input value is validated against the collection of validators.
        /// </summary>
        public event InputValidatedEventHandler Validated;

        /// <summary>
        /// Gets or sets the style associated with the feedback message.
        /// </summary>
        public Style FeedbackMessageStyle
        {
            get => (Style)this.GetValue(FeedbackMessageStyleProperty);
            set => this.SetValue(FeedbackMessageStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the input to run validation against.
        /// </summary>
        public object Input
        {
            get => this.GetValue(InputProperty);
            set => this.SetValue(InputProperty, value);
        }

        /// <summary>
        /// Gets or sets the validators to run on the input.
        /// </summary>
        public ValidatorCollection Validators
        {
            get => (ValidatorCollection)this.GetValue(ValidatorsProperty);
            set => this.SetValue(ValidatorsProperty, value);
        }

        /// <summary>
        /// Gets the view representing the validator feedback message.
        /// </summary>
        public TextBlock ValidatorFeedbackMessage { get; private set; }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.ValidatorFeedbackMessage = this.GetChildView<TextBlock>(ValidatorFeedbackMessagePart);
            this.InvokeValidators();
        }

        /// <summary>
        /// Provides the class-specific <see cref="InputValidatorAutomationPeer"/> implementation for the Microsoft UI Automation infrastructure.
        /// </summary>
        /// <returns>The class-specific <see cref="InputValidatorAutomationPeer"/> instance.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new InputValidatorAutomationPeer(this);
        }

        private void InvokeValidators()
        {
            if (this.Validators == null)
            {
                return;
            }

            this.Validators.Validate(this.Input);
            this.Validated?.Invoke(
                this,
                new InputValidatedEventArgs(this.Validators.IsInvalid, this.Validators.IsDirty));

            if (this.ValidatorFeedbackMessage != null)
            {
                this.ValidatorFeedbackMessage.Text = string.Join("\r\n", this.Validators.FeedbackMessages);
                this.ValidatorFeedbackMessage.SetVisible(!this.ValidatorFeedbackMessage.Text.IsNullOrWhiteSpace());
            }
        }
    }
}