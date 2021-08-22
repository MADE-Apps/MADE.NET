namespace MADE.UI.Styling.Colors
{
    using System.Globalization;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines a collection of extensions for Windows colors.
    /// </summary>
    public static class WindowsColorExtensions
    {
        /// <summary>
        /// Converts a <see cref="T:Windows.UI.Color"/> to a <see cref="T:System.Drawing.Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="T:Windows.UI.Color" /> to convert.</param>
        /// <returns>A The <see cref="T:System.Drawing.Color" />.</returns>
        public static System.Drawing.Color ToSystemColor(this Windows.UI.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts the specified <see cref="T:Windows.UI.Color" /> to a <see cref="T:Windows.UI.Xaml.Media.SolidColorBrush" />.
        /// </summary>
        /// <param name="color">
        /// The <see cref="T:Windows.UI.Color" /> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="T:Windows.UI.Xaml.Media.SolidColorBrush" /> representation of the specified color.
        /// </returns>
        public static SolidColorBrush ToSolidColorBrush(this Windows.UI.Color color)
        {
            return new SolidColorBrush(color);
        }

        /// <summary>
        /// Gets the hex representation of the specified <see cref="T:Windows.UI.Color" />.
        /// </summary>
        /// <param name="color">
        /// The <see cref="T:Windows.UI.Color" /> to get the hex value of.
        /// </param>
        /// <returns>
        /// The hex value as a <see cref="T:System.String" />.
        /// </returns>
        public static string ToHexString(this Windows.UI.Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Converts an ARGB or RGB hex value to a <see cref="T:Windows.UI.Color" />.
        /// </summary>
        /// <param name="hexValue">
        /// The ARGB or RGB hex value represented as a string.
        /// </param>
        /// <returns>
        /// The Color representation of the ARGB hex string value.
        /// </returns>
        public static Windows.UI.Color ToWindowsColor(this string hexValue)
        {
            string upper = hexValue.ToUpper();

            Windows.UI.Color color = upper.Length switch
            {
                7 => new Windows.UI.Color
                {
                    A = byte.MaxValue,
                    R = byte.Parse(upper.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                    G = byte.Parse(upper.Substring(3, 2), NumberStyles.AllowHexSpecifier),
                    B = byte.Parse(upper.Substring(5, 2), NumberStyles.AllowHexSpecifier),
                },
                9 => new Windows.UI.Color
                {
                    A = byte.Parse(upper.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                    R = byte.Parse(upper.Substring(3, 2), NumberStyles.AllowHexSpecifier),
                    G = byte.Parse(upper.Substring(5, 2), NumberStyles.AllowHexSpecifier),
                    B = byte.Parse(upper.Substring(7, 2), NumberStyles.AllowHexSpecifier),
                },
                _ => Windows.UI.Colors.Transparent
            };

            return color;
        }

        /// <summary>Lightens a color by a given amount.</summary>
        /// <param name="color">
        /// The <see cref="T:Windows.UI.Color" /> to lighten.
        /// </param>
        /// <param name="amount">The amount to lighten by.</param>
        /// <returns>
        /// The lightened color as a <see cref="T:Windows.UI.Color" />.
        /// </returns>
        public static Windows.UI.Color Lighten(this Windows.UI.Color color, float amount)
        {
            double num = amount * 0.01;
            return color.Lerp((float)num);
        }

        /// <summary>Darkens a color by a given amount.</summary>
        /// <param name="color">
        /// The <see cref="T:Windows.UI.Color" /> to darken.
        /// </param>
        /// <param name="amount">The amount to darken by.</param>
        /// <returns>
        /// The darkened color as a <see cref="T:Windows.UI.Color" />.
        /// </returns>
        public static Windows.UI.Color Darken(this Windows.UI.Color color, float amount)
        {
            double num = amount * -0.01;
            return color.Lerp((float)num);
        }

        /// <summary>
        /// Checks whether the specified color is within the range of another by the specified amount.
        /// </summary>
        /// <param name="color">
        /// The color to compare.
        /// </param>
        /// <param name="comparer">
        /// The color to compare with.
        /// </param>
        /// <param name="amount">
        /// The range either side of the <paramref name="comparer"/>.
        /// </param>
        /// <returns>
        /// True if is in range; otherwise, false.
        /// </returns>
        public static bool IsInRange(this Windows.UI.Color color, Windows.UI.Color comparer, float amount)
        {
            return IsInRange(color, comparer, amount, false);
        }

        /// <summary>
        /// Checks whether the specified color is within the range of another by the specified amount.
        /// </summary>
        /// <param name="color">
        /// The color to compare.
        /// </param>
        /// <param name="comparer">
        /// The color to compare with.
        /// </param>
        /// <param name="amount">
        /// The range either side of the <paramref name="comparer"/>.
        /// </param>
        /// <param name="ignoreTransparency">
        /// Indicates whether to ignore transparency.
        /// </param>
        /// <returns>
        /// True if is in range; otherwise, false.
        /// </returns>
        public static bool IsInRange(this Windows.UI.Color color, Windows.UI.Color comparer, float amount, bool ignoreTransparency)
        {
            bool isInRange = false;

            Windows.UI.Color colorToCompare = ignoreTransparency ? Windows.UI.Color.FromArgb(255, color.R, color.G, color.B) : color;

            for (int i = 0; i < amount; i++)
            {
                if (colorToCompare == comparer)
                {
                    isInRange = true;
                    break;
                }

                Windows.UI.Color darken = comparer.Darken(i);
                Windows.UI.Color lighten = comparer.Lighten(i);

                if (colorToCompare == darken)
                {
                    isInRange = true;
                    break;
                }

                if (colorToCompare == lighten)
                {
                    isInRange = true;
                    break;
                }
            }

            return isInRange;
        }

        private static Windows.UI.Color Lerp(this Windows.UI.Color color, float amount)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (amount < 0)
            {
                amount = 1 + amount;
                red *= amount;
                green *= amount;
                blue *= amount;
            }
            else
            {
                red = (255 - red) * amount + red;
                green = (255 - green) * amount + green;
                blue = (255 - blue) * amount + blue;
            }

            return Windows.UI.Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}