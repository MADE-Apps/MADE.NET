/*
 * This control is a simplified version of the ControlExample from the XAML Controls Gallery application.
 * https://github.com/microsoft/Xaml-Controls-Gallery/blob/master/XamlControlsGallery/ControlExample.xaml
 *
 * It is being used here to render XAML and C# code samples with syntax highlighting.
 *
 * The original code is made available under the project's MIT License (https://github.com/microsoft/Xaml-Controls-Gallery/blob/master/LICENSE).
 */

namespace MADE.Samples.Infrastructure.Controls
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ColorCode;
    using ColorCode.Common;
    using MADE.Samples.Infrastructure.Styling;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    public sealed partial class SampleControl : UserControl
    {
        internal const string CodeFont = "Consolas";
        internal const char NewLine = '\n';

        public static readonly DependencyProperty SampleNameProperty = DependencyProperty.Register(
            nameof(SampleName),
            typeof(string),
            typeof(SampleControl),
            new PropertyMetadata(default(string)));

        public static readonly DependencyProperty SampleDescriptionProperty = DependencyProperty.Register(
            nameof(SampleDescription),
            typeof(string),
            typeof(SampleControl),
            new PropertyMetadata(default(string)));

        public static readonly DependencyProperty SampleProperty = DependencyProperty.Register(
            nameof(Sample),
            typeof(object),
            typeof(SampleControl),
            new PropertyMetadata(default));

        public static readonly DependencyProperty XamlSourceProperty = DependencyProperty.Register(
            nameof(XamlSource),
            typeof(object),
            typeof(SampleControl),
            new PropertyMetadata(default(Uri), (o, args) => ((SampleControl)o).GenerateSyntaxHighlightedContentForXaml()));

        public static readonly DependencyProperty CodeSourceProperty = DependencyProperty.Register(
            nameof(CodeSource),
            typeof(object),
            typeof(SampleControl),
            new PropertyMetadata(default(Uri), (o, args) => ((SampleControl)o).GenerateSyntaxHighlightedContentForCode()));

        public SampleControl()
        {
            this.InitializeComponent();
        }

        public string SampleName
        {
            get => (string)GetValue(SampleNameProperty);
            set => SetValue(SampleNameProperty, value);
        }

        public string SampleDescription
        {
            get => (string)GetValue(SampleDescriptionProperty);
            set => SetValue(SampleDescriptionProperty, value);
        }

        public object Sample
        {
            get => this.GetValue(SampleProperty);
            set => SetValue(SampleProperty, value);
        }

        public Uri XamlSource
        {
            get => (Uri)GetValue(XamlSourceProperty);
            set => SetValue(XamlSourceProperty, value);
        }

        public Uri CodeSource
        {
            get => (Uri)GetValue(CodeSourceProperty);
            set => SetValue(CodeSourceProperty, value);
        }

        private static Uri GetApplicationSourceUri(Uri rawSource)
        {
            // Get the full path of the source string
            var concatString = string.Empty;
            for (var i = 2; i < rawSource.Segments.Length; i++)
            {
                concatString += rawSource.Segments[i];
            }

            var derivedSource = new Uri(new Uri("ms-appx:///Features/Samples/Assets/"), concatString);

            return derivedSource;
        }

        private static void UpdateFormatterForDarkTheme(CodeColorizerBase colorizer)
        {
            // Replace the default dark theme resources with ones that more closely align to VS Code dark theme.
            colorizer.Styles.Remove(colorizer.Styles[ScopeName.XmlAttribute]);
            colorizer.Styles.Remove(colorizer.Styles[ScopeName.XmlAttributeQuotes]);
            colorizer.Styles.Remove(colorizer.Styles[ScopeName.XmlAttributeValue]);
            colorizer.Styles.Remove(colorizer.Styles[ScopeName.HtmlComment]);
            colorizer.Styles.Remove(colorizer.Styles[ScopeName.XmlDelimiter]);
            colorizer.Styles.Remove(colorizer.Styles[ScopeName.XmlName]);

            colorizer.Styles.Add(new ColorCode.Styling.Style(ScopeName.XmlAttribute)
            {
                Foreground = "#FF87CEFA",
                ReferenceName = "xmlAttribute"
            });
            colorizer.Styles.Add(new ColorCode.Styling.Style(ScopeName.XmlAttributeQuotes)
            {
                Foreground = "#FFFFA07A",
                ReferenceName = "xmlAttributeQuotes"
            });
            colorizer.Styles.Add(new ColorCode.Styling.Style(ScopeName.XmlAttributeValue)
            {
                Foreground = "#FFFFA07A",
                ReferenceName = "xmlAttributeValue"
            });
            colorizer.Styles.Add(new ColorCode.Styling.Style(ScopeName.HtmlComment)
            {
                Foreground = "#FF6B8E23",
                ReferenceName = "htmlComment"
            });
            colorizer.Styles.Add(new ColorCode.Styling.Style(ScopeName.XmlDelimiter)
            {
                Foreground = "#FF808080",
                ReferenceName = "xmlDelimiter"
            });
            colorizer.Styles.Add(new ColorCode.Styling.Style(ScopeName.XmlName)
            {
                Foreground = "#FF5F82E8",
                ReferenceName = "xmlName"
            });
        }

        private void GenerateSyntaxHighlightedContentForXaml()
        {
            GenerateSyntaxHighlightedContentFromFileAsync(this.XamlSource, this.SampleXamlPresenter, Languages.Xml);
        }

        private void GenerateSyntaxHighlightedContentForCode()
        {
            GenerateSyntaxHighlightedContentFromFileAsync(this.CodeSource, this.SampleCodePresenter, Languages.CSharp);
        }

        private async Task GenerateSyntaxHighlightedContentFromFileAsync(Uri source, Panel presenter, ILanguage syntaxHighlighting)
        {
            if (source != null && source.AbsolutePath.EndsWith("txt"))
            {
                presenter.Visibility = Visibility.Visible;

                var absoluteSourceUri = GetApplicationSourceUri(source);
                var file = await StorageFile.GetFileFromApplicationUriAsync(absoluteSourceUri);
                var content = await FileIO.ReadTextAsync(file);

                this.GenerateSyntaxHighlightedContentFromString(content, presenter, syntaxHighlighting);
            }
            else
            {
                presenter.Visibility = Visibility.Collapsed;
            }
        }

        private void GenerateSyntaxHighlightedContentFromString(string content, Panel presenter, ILanguage syntaxHighlighting)
        {
            content = content.TrimStart(NewLine).TrimEnd();
            content = string.Join("\n", content.Split(NewLine).Select(s => s.TrimEnd()));

            var codeRichTextBlock = new RichTextBlock { FontFamily = new FontFamily(CodeFont) };

            var formatter = GenerateRichTextFormatter();
            formatter.FormatRichTextBlock(content, syntaxHighlighting, codeRichTextBlock);
            presenter.Children.Clear();
            presenter.Children.Add(codeRichTextBlock);
        }

        private RichTextBlockFormatter GenerateRichTextFormatter()
        {
            var formatter = new RichTextBlockFormatter();

            if (ThemeHelper.CurrentTheme == ElementTheme.Dark)
            {
                UpdateFormatterForDarkTheme(formatter);
            }

            return formatter;
        }
    }
}