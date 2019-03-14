// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextEntry.xaml.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element representing a text entry with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views
{
    using System;

    using MADE.App.Views.Extensions;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using XPlat.UI.Controls;

    /// <summary>
    /// Defines a UI element representing a text entry with a header component.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextEntry : ContentView
    {
        /// <summary>
        /// Defines the bindable property for the <see cref="Header"/> value.
        /// </summary>
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
            nameof(Header),
            typeof(string),
            typeof(TextEntry),
            null,
            propertyChanged: (bindable, ValueChangedEventArgs, newValue) =>
                {
                    var control = (TextEntry)bindable;
                    control.TextEntryHeader.Text = (string)newValue;
                    control.TextEntryHeader.IsVisible = !string.IsNullOrWhiteSpace(control.Header);
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="HeaderStyle"/> value.
        /// </summary>
        public static readonly BindableProperty HeaderStyleProperty = BindableProperty.Create(
            nameof(HeaderStyle),
            typeof(Style),
            typeof(TextEntry),
            default(Style),
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (TextEntry)bindable;
                    control.TextEntryHeader.Style = (Style)newValue;
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="Text"/> value.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TextEntry),
            null,
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (TextEntry)bindable;
                    var text = (string)newValue;
                    if (control.TextEntryContent.Text != text)
                    {
                        control.TextEntryContent.Text = text;
                    }
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="TextStyle"/> value.
        /// </summary>
        public static readonly BindableProperty TextStyleProperty = BindableProperty.Create(
            nameof(TextStyle),
            typeof(Style),
            typeof(TextEntry),
            default(Style),
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (TextEntry)bindable;
                    control.TextEntryContent.Style = (Style)newValue;
                });

        /// <summary>
        /// Defines the bindable property for the <see cref="Orientation"/> value.
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            nameof(Orientation),
            typeof(Orientation),
            typeof(TextEntry),
            Orientation.Vertical,
            propertyChanged: (bindable, value, newValue) =>
                {
                    var control = (TextEntry)bindable;
                    control.UpdateOrientation();
                });

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEntry"/> class.
        /// </summary>
        public TextEntry()
        {
            this.InitializeComponent();
            this.TextEntryContent.TextChanged += this.OnTextChanged;
        }

        public event EventHandler<TextChangedEventArgs> TextChanged;

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
        /// Updates the layout for the control based on the current <see cref="XPlat.UI.Controls.Orientation"/> value.
        /// </summary>
        public void UpdateOrientation()
        {
            this.TextEntryContainer.SetOrientation(this.Orientation);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.Text = e.NewTextValue;
            this.TextChanged?.Invoke(this, e);
        }
    }
}