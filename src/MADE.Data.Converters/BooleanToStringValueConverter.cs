namespace MADE.Data.Converters
{
    using MADE.Data.Converters.Exceptions;

    /// <summary>
    /// Defines a value converter from <see cref="bool"/> to <see cref="string"/> with a pre-determined <see cref="TrueValue"/> and <see cref="FalseValue"/>.
    /// </summary>
    public partial class BooleanToStringValueConverter : IValueConverter<bool, string>
    {
#if !WINDOWS_UWP
        /// <summary>
        /// Gets or sets the positive/true value.
        /// </summary>
        public string TrueValue { get; set; }

        /// <summary>
        /// Gets or sets the negative/false value.
        /// </summary>
        public string FalseValue { get; set; }
#endif

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> to the <see cref="string"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="string"/> object.
        /// </returns>
        public string Convert(bool value, object parameter = default)
        {
            return value ? this.TrueValue : this.FalseValue;
        }

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> back to the <see cref="bool"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="bool"/> object.
        /// </returns>
        public bool ConvertBack(string value, object parameter = default)
        {
            if (value == this.TrueValue)
            {
                return true;
            }

            if (value == this.FalseValue)
            {
                return false;
            }

            throw new InvalidDataConversionException(nameof(BooleanToStringValueConverter), value, $"The value to convert back is not of the expected {nameof(this.TrueValue)} or {nameof(this.FalseValue)}");
        }
    }
}
