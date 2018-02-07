// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatingTextBox.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element allowing the user to input a value and have real-time validation run against it.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System.Windows.Input;

    using MADE.Data.Validation.Rules;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a UI element allowing the user to input a value and have real-time validation run against it.
    /// </summary>
    public partial class ValidatingTextBox : TextBox, IValidatingTextBox
    {
        public static readonly DependencyProperty ValidationUpdatedCommandProperty = DependencyProperty.Register(
            nameof(ValidationUpdatedCommand),
            typeof(ICommand),
            typeof(ValidatingTextBox),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty IsMandatoryProperty = DependencyProperty.Register(
            nameof(IsMandatory),
            typeof(bool),
            typeof(ValidatingTextBox),
            new PropertyMetadata(false, (d, e) => ((ValidatingTextBox)d).Update()));

        public static readonly DependencyProperty MandatoryValidationMessageProperty = DependencyProperty.Register(
            nameof(MandatoryValidationMessage),
            typeof(string),
            typeof(ValidatingTextBox),
            new PropertyMetadata("Required"));

        public static readonly DependencyProperty IsControlValidProperty = DependencyProperty.Register(
            nameof(IsControlValid),
            typeof(bool),
            typeof(ValidatingTextBox),
            new PropertyMetadata(false));

        public static readonly DependencyProperty ValidationRulesProperty = DependencyProperty.Register(
            nameof(ValidationRules),
            typeof(ValidationRules),
            typeof(ValidatingTextBox),
            new PropertyMetadata(default(ValidationRules), (d, e) => ((ValidatingTextBox)d).Update()));

        private TextBlock remainingCharactersTextBlock;

        private TextBlock validationMessageTextBlock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatingTextBox"/> class.
        /// </summary>
        public ValidatingTextBox()
        {
            this.DefaultStyleKey = typeof(ValidatingTextBox);

            this.Loaded += this.OnLoaded;
        }

        /// <summary>
        /// The event associated with the control's validation being updated.
        /// </summary>
        public event ValidationUpdatedEventHandler ValidationUpdated;

        /// <summary>
        /// Gets or sets the command called when the control's validation has updated.
        /// </summary>
        public ICommand ValidationUpdatedCommand
        {
            get => (ICommand)this.GetValue(ValidationUpdatedCommandProperty);
            set => this.SetValue(ValidationUpdatedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is mandatory, requiring a value.
        /// </summary>
        public bool IsMandatory
        {
            get => (bool)this.GetValue(IsMandatoryProperty);
            set => this.SetValue(IsMandatoryProperty, value);
        }

        /// <summary>
        /// Gets or sets the message displayed when the mandatory validation is not met.
        /// </summary>
        public string MandatoryValidationMessage
        {
            get => (string)this.GetValue(MandatoryValidationMessageProperty);
            set => this.SetValue(MandatoryValidationMessageProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is currently in a valid state.
        /// </summary>
        public bool IsControlValid
        {
            get => (bool)this.GetValue(IsControlValidProperty);
            set => this.SetValue(IsControlValidProperty, value);
        }

        /// <summary>
        /// Gets or sets the validation rules to run against the value of the text box.
        /// </summary>
        public ValidationRules ValidationRules
        {
            get => (ValidationRules)this.GetValue(ValidationRulesProperty);
            set => this.SetValue(ValidationRulesProperty, value);
        }

        /// <summary>
        /// Re-checks and updates the current valid state of the control.
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.remainingCharactersTextBlock = this.GetTemplateChild<TextBlock>("RemainingCharactersContent");
            this.validationMessageTextBlock = this.GetTemplateChild<TextBlock>("ValidationMessageContent");

            // ToDo
        }
    }
}