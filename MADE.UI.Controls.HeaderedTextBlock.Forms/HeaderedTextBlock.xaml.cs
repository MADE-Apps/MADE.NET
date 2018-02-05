namespace MADE.UI.Control.Forms
{
    using MADE.UI.Controls;
    using MADE.UI.Controls.Forms;
    using MADE.UI.Layout;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderedTextBlock : IHeaderedTextBlock
    {
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

        public HeaderedTextBlock()
        {
            this.InitializeComponent();
        }

        public event ControlLoadedEventHandler ControlLoaded;

        public string Header
        {
            get => (string)this.GetValue(HeaderProperty);
            set => this.SetValue(HeaderProperty, value);
        }

        public Style HeaderStyle
        {
            get => (Style)this.GetValue(HeaderStyleProperty);
            set => this.SetValue(HeaderStyleProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public Style TextStyle
        {
            get => (Style)this.GetValue(TextStyleProperty);
            set => this.SetValue(TextStyleProperty, value);
        }

        public Orientation Orientation
        {
            get => (Orientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
        }

        public bool HideIfNullOrWhiteSpace
        {
            get => (bool)this.GetValue(HideIfNullOrWhiteSpaceProperty);
            set => this.SetValue(HideIfNullOrWhiteSpaceProperty, value);
        }

        public void UpdateOrientation()
        {
            this.HeaderTextBlockContainer.Orientation = this.Orientation.ToStackOrientation();
        }

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