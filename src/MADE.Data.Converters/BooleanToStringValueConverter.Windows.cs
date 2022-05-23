// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINDOWS_UWP
namespace MADE.Data.Converters
{
    using System;
    using MADE.Data.Converters.Strings;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a Windows components for a XAML value converter from <see cref="bool"/> to <see cref="string"/>.
    /// </summary>
    [Obsolete("BooleanToStringValueConverter for Windows will be removed in a future major release. Use the replicated BooleanToStringValueConverter type from the MADE.UI.Data.Converters library instead.")]
    public partial class BooleanToStringValueConverter : DependencyObject, IValueConverter, IValueConverter<bool, string>
    {
        /// <summary>
        /// Defines the dependency property for <see cref="TrueValue"/>.
        /// </summary>
        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register(
                nameof(TrueValue),
                typeof(string),
                typeof(BooleanToStringValueConverter),
                new PropertyMetadata(Resources.BooleanToStringValueConverter_TrueValue));

        /// <summary>
        /// Defines the dependency property for <see cref="FalseValue"/>.
        /// </summary>
        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register(
                nameof(FalseValue),
                typeof(string),
                typeof(BooleanToStringValueConverter),
                new PropertyMetadata(Resources.BooleanToStringValueConverter_FalseValue));

        /// <summary>
        /// Gets or sets the positive/true value.
        /// </summary>
        public string TrueValue
        {
            get => (string)this.GetValue(TrueValueProperty);
            set => this.SetValue(TrueValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the negative/false value.
        /// </summary>
        public string FalseValue
        {
            get => (string)this.GetValue(FalseValueProperty);
            set => this.SetValue(FalseValueProperty, value);
        }

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> to the <see cref="string"/> type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type (unused).</param>
        /// <param name="parameter">The optional parameter used to help with conversion (unused).</param>
        /// <param name="language">The display language for the conversion (unused).</param>
        /// <returns>The converted <see cref="string"/> object.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                bool b => this.Convert(b, parameter),
                _ => value
            };
        }

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> back to the <see cref="bool"/> type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type (unused).</param>
        /// <param name="parameter">The optional parameter used to help with conversion (unused).</param>
        /// <param name="language">The display language for the conversion (unused).</param>
        /// <returns>The converted <see cref="bool"/> object.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is not string b)
            {
                return value;
            }

            return this.ConvertBack(b, parameter);
        }
    }
}
#endif