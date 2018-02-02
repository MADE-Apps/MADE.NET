// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderedTextBlock.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element representing read-only text with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Orientation = MADE.UI.Layout.Orientation;

    /// <summary>
    /// Defines a UI element representing read-only text with a header component.
    /// </summary>
    [TemplatePart(Name = "HeaderContent", Type = typeof(TextBlock))]
    [TemplatePart(Name = "TextContent", Type = typeof(TextBlock))]
    [TemplateVisualState(GroupName = "OrientationStates", Name = "Vertical")]
    [TemplateVisualState(GroupName = "OrientationStates", Name = "Horizontal")]
    public class HeaderedTextBlock : Control, IHeaderedTextBlock
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(null, (d, e) => ((HeaderedTextBlock)d).UpdateVisibility()));

        public static readonly DependencyProperty HeaderStyleProperty = DependencyProperty.Register(
            nameof(HeaderStyle),
            typeof(Style),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(Orientation.Vertical, (d, e) => ((HeaderedTextBlock)d).UpdateOrientation()));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(null, (d, e) => ((HeaderedTextBlock)d).UpdateVisibility()));

        public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(
            nameof(TextStyle),
            typeof(Style),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty HideIfNullOrWhiteSpaceProperty = DependencyProperty.Register(
            nameof(HideIfNullOrWhiteSpace),
            typeof(bool),
            typeof(HeaderedTextBlock),
            new PropertyMetadata(false, (d, e) => ((HeaderedTextBlock)d).UpdateVisibility()));

        private TextBlock headerTextBlock;

        private TextBlock textTextBlock;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        public HeaderedTextBlock()
        {
            this.DefaultStyleKey = typeof(HeaderedTextBlock);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the control if the <see cref="IHeaderedTextBlock.Text"/> is null or whitespace.
        /// </summary>
        public bool HideIfNullOrWhiteSpace
        {
            get => (bool)this.GetValue(HideIfNullOrWhiteSpaceProperty);
            set => this.SetValue(HideIfNullOrWhiteSpaceProperty, value);
        }

        /// <summary>
        /// Gets or sets the string associated with the text.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the orientation the header and text should layout as.
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the string associated with the header.
        /// </summary>
        public string Header
        {
            get => (string)this.GetValue(HeaderProperty);
            set => this.SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the style associated with the header content.
        /// </summary>
        public Style HeaderStyle
        {
            get => (Style)this.GetValue(HeaderStyleProperty);
            set => this.SetValue(HeaderStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style associated with the text content.
        /// </summary>
        public Style TextStyle
        {
            get => (Style)this.GetValue(TextStyleProperty);
            set => this.SetValue(TextStyleProperty, value);
        }

        /// <summary>
        /// Updates the layout for the control based on the current <see cref="IHeaderedTextBlock.Orientation"/> value.
        /// </summary>
        public void UpdateOrientation()
        {
            switch (this.Orientation)
            {
                case Orientation.Vertical:
                    VisualStateManager.GoToState(this, "Vertical", true);
                    break;
                case Orientation.Horizontal:
                    VisualStateManager.GoToState(this, "Horizontal", true);
                    break;
            }
        }

        /// <summary>
        /// Updates the visibility of the control based on the values of the <see cref="IHeaderedTextBlock.Header"/> and <see cref="IHeaderedTextBlock.Text"/> properties.
        /// </summary>
        public void UpdateVisibility()
        {
            if (!this.HideIfNullOrWhiteSpace || !string.IsNullOrWhiteSpace(this.Text))
            {
                this.IsVisible = true;
                this.headerTextBlock?.SetVisible(!string.IsNullOrWhiteSpace(this.Header));
                this.textTextBlock?.SetVisible(!string.IsNullOrWhiteSpace(this.Text));
            }
            else
            {
                this.IsVisible = false;
                this.headerTextBlock?.SetVisible(false);
                this.textTextBlock?.SetVisible(false);
            }
        }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.headerTextBlock = this.GetTemplateChild<TextBlock>("HeaderContent");
            this.textTextBlock = this.GetTemplateChild<TextBlock>("TextContent");

            this.UpdateVisibility();
            this.UpdateOrientation();
        }
    }
}