// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderedTextBlock.xaml.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element representing read-only text with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Control.Forms
{
    using MADE.UI.Controls;
    using MADE.UI.Controls.Forms;
    using MADE.UI.Layout;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Defines a UI element representing read-only text with a header component.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderedTextBlock : IHeaderedTextBlock
    {
        /// <summary>
        /// Defines the bindable property for the <see cref="Header"/> value.
        /// </summary>
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
            nameof(Header),
            typeof(string),
            typeof(HeaderedTextBlock),
            null,
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (HeaderedTextBlock)bindable;
                    control.HeaderContent.Text = (string)newValue;
                    control.UpdateVisibility();
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="HeaderStyle"/> value.
        /// </summary>
        public static readonly BindableProperty HeaderStyleProperty = BindableProperty.Create(
            nameof(HeaderStyle),
            typeof(Style),
            typeof(HeaderedTextBlock),
            default(Style),
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (HeaderedTextBlock)bindable;
                    control.HeaderContent.Style = (Style)newValue;
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="Orientation"/> value.
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            nameof(Orientation),
            typeof(Orientation),
            typeof(HeaderedTextBlock),
            Orientation.Vertical,
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (HeaderedTextBlock)bindable;
                    control.UpdateOrientation();
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="Text"/> value.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(HeaderedTextBlock),
            null,
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (HeaderedTextBlock)bindable;
                    control.TextContent.Text = (string)newValue;
                    control.UpdateVisibility();
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="TextStyle"/> value.
        /// </summary>
        public static readonly BindableProperty TextStyleProperty = BindableProperty.Create(
            nameof(TextStyle),
            typeof(Style),
            typeof(HeaderedTextBlock),
            default(Style),
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (HeaderedTextBlock)bindable;
                    control.TextContent.Style = (Style)newValue;
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="HideIfNullOrWhiteSpace"/> value.
        /// </summary>
        public static readonly BindableProperty HideIfNullOrWhiteSpaceProperty = BindableProperty.Create(
            nameof(HideIfNullOrWhiteSpace),
            typeof(bool),
            typeof(HeaderedTextBlock),
            false,
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (HeaderedTextBlock)bindable;
                    control.UpdateVisibility();
                });

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        public HeaderedTextBlock()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The event associated with the control being loaded.
        /// </summary>
        public event ControlLoadedEventHandler ControlLoaded;

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
        /// Gets or sets the string associated with the text.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
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
        /// Gets or sets the orientation the header and text should layout as.
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
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
        /// Updates the layout for the control based on the current <see cref="IHeaderedTextBlock.Orientation"/> value.
        /// </summary>
        public void UpdateOrientation()
        {
            this.HeaderTextBlockContainer.SetOrientation(this.Orientation);
        }

        /// <summary>
        /// Updates the visibility of the control based on the values of the <see cref="IHeaderedTextBlock.Header"/> and <see cref="IHeaderedTextBlock.Text"/> properties.
        /// </summary>
        public void UpdateVisibility()
        {
            if (!this.HideIfNullOrWhiteSpace || !string.IsNullOrWhiteSpace(this.Text))
            {
                this.IsVisible = true;
                this.HeaderContent?.SetVisible(!string.IsNullOrWhiteSpace(this.Header));
                this.TextContent?.SetVisible(!string.IsNullOrWhiteSpace(this.Text));
            }
            else
            {
                this.IsVisible = false;
                this.HeaderContent?.SetVisible(false);
                this.TextContent?.SetVisible(false);
            }
        }
    }
}