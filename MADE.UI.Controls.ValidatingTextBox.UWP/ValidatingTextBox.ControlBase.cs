// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatingTextBox.ControlBase.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines the base control logic for the ValidatingTextBox.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using MADE.UI.Design;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines the base control logic for the <see cref="ValidatingTextBox"/>.
    /// </summary>
    public partial class ValidatingTextBox : IWindowsControl
    {
        /// <summary>
        /// The event associated with the control being loaded.
        /// </summary>
        public event ControlLoadedEventHandler ControlLoaded;

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible to the user.
        /// </summary>
        public bool IsVisible
        {
            get => this.Visibility == Visibility.Visible;
            set => this.SetVisible(value);
        }

        /// <summary>
        /// Gets or sets a color that provides the background of the control.
        /// </summary>
        public Color BackgroundColor
        {
            get => this.Background as SolidColorBrush;
            set => this.Background = value;
        }

        /// <summary>
        /// Retrieves the given named element from the instantiated control template.
        /// </summary>
        /// <param name="name">
        /// The name of the element to find.
        /// </param>
        /// <typeparam name="TElement">
        /// The type of element to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the element from the template, if the element is found.
        /// </returns>
        public TElement GetTemplateChild<TElement>(string name)
            where TElement : class
        {
            return this.GetTemplateChild(name) as TElement;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;
            this.ControlLoaded?.Invoke(this, new ControlLoadedEventArgs());
        }
    }
}