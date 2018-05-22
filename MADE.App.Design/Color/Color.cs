// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Color.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a UI element color.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Design.Color
{
    /// <summary>
    /// Defines a model for a UI element color.
    /// </summary>
    public class Color : IColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class with set ARGB byte values.
        /// </summary>
        /// <param name="a">
        /// The alpha value.
        /// </param>
        /// <param name="r">
        /// The red value.
        /// </param>
        /// <param name="g">
        /// The green value.
        /// </param>
        /// <param name="b">
        /// The blue value.
        /// </param>
        internal Color(byte a, byte r, byte g, byte b)
        {
            this.A = a;
            this.R = r;
            this.G = g;
            this.B = b;
        }

#if WINDOWS_UWP || __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class with a <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="color">
        /// The system color value.
        /// </param>
        public Color(System.Drawing.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        /// <summary>
        /// Supports the conversion of a <see cref="System.Drawing.Color"/> to <see cref="Color"/> implicitly.
        /// </summary>
        /// <param name="color">
        /// The system color value.
        /// </param>
        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color);
        }

        /// <summary>
        /// Supports the conversion of a <see cref="Color"/> to <see cref="System.Drawing.Color"/> implicitly.
        /// </summary>
        /// <param name="color">
        /// The internal color value.
        /// </param>
        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
#endif

#if __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class with a <see cref="Android.Graphics.Color"/>.
        /// </summary>
        /// <param name="color">
        /// The Android color value.
        /// </param>
        public Color(Android.Graphics.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        /// <summary>
        /// Supports the conversion of a <see cref="Android.Graphics.Color"/> to <see cref="Color"/> implicitly.
        /// </summary>
        /// <param name="color">
        /// The Android color value.
        /// </param>
        public static implicit operator Color(Android.Graphics.Color color)
        {
            return new Color(color);
        }

        /// <summary>
        /// Supports the conversion of a <see cref="Color"/> to <see cref="Android.Graphics.Color"/> implicitly.
        /// </summary>
        /// <param name="color">
        /// The internal color value.
        /// </param>
        public static implicit operator Android.Graphics.Color(Color color)
        {
            return new Android.Graphics.Color(color.R, color.G, color.B, color.A);
        }
#endif

#if WINDOWS_UWP
        public Color(Windows.UI.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public static implicit operator Color(Windows.UI.Color color)
        {
            return new Color(color);
        }

        public static implicit operator Color(Windows.UI.Xaml.Media.SolidColorBrush colorBrush)
        {
            return new Color(colorBrush.Color);
        }

        public static implicit operator Windows.UI.Color(Color color)
        {
            return Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static implicit operator Windows.UI.Xaml.Media.SolidColorBrush(Color color)
        {
            return new Windows.UI.Xaml.Media.SolidColorBrush(color);
        }
#endif

#if __IOS__
        public Color(UIKit.UIColor color)
        {
            if (color != null && color.CGColor != null)
            {
                if (color.CGColor.NumberOfComponents == 2)
                {
                    var rgb = (byte)(color.CGColor.Components[0] * 255);
                    this.R = rgb;
                    this.G = rgb;
                    this.B = rgb;
                    this.A = (byte)(color.CGColor.Components[1] * 255);
                }
                else
                {
                    this.R = (byte)(color.CGColor.Components[0] * 255);
                    this.G = (byte)(color.CGColor.Components[1] * 255);
                    this.B = (byte)(color.CGColor.Components[2] * 255);
                    this.A = (byte)(color.CGColor.Components[3] * 255);
                }
            }
        }

        public static implicit operator Color(UIKit.UIColor color)
        {
            return new Color(color);
        }

        public static implicit operator UIKit.UIColor(Color color)
        {
            return UIKit.UIColor.FromRGBA(color.R, color.G, color.B, color.A);
        }
#endif

        /// <summary>
        /// Gets or sets the alpha component of the color.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// Gets or sets the red component of the color.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Gets or sets the green component of the color.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Gets or sets the blue component of the color.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Constructs a color based on the alpha, red, green and blue parameters.
        /// </summary>
        /// <param name="a">
        /// The alpha component.
        /// </param>
        /// <param name="r">
        /// The red component.
        /// </param>
        /// <param name="g">
        /// The green component.
        /// </param>
        /// <param name="b">
        /// The blue component.
        /// </param>
        /// <returns>
        /// Returns the constructed color.
        /// </returns>
        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color(a, r, g, b);
        }

        /// <summary>
        /// Checks the equality of two color items.
        /// </summary>
        /// <param name="color1">
        /// The color 1.
        /// </param>
        /// <param name="color2">
        /// The color 2.
        /// </param>
        /// <returns>
        /// Return true if the colors are equal.
        /// </returns>
        public static bool operator ==(Color color1, Color color2)
        {
            if (ReferenceEquals(color1, color2))
            {
                return true;
            }

            if (ReferenceEquals(color1, null))
            {
                return false;
            }

            if (ReferenceEquals(color2, null))
            {
                return false;
            }

            return color1.Equals(color2);
        }

        /// <summary>
        /// Checks the inequality of two color items.
        /// </summary>
        /// <param name="color1">
        /// The color 1.
        /// </param>
        /// <param name="color2">
        /// The color 2.
        /// </param>
        /// <returns>
        /// Returns true if the colors are not equal.
        /// </returns>
        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// Returns true if the specified object is equal to the current object.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((Color)obj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.A.GetHashCode();
                hashCode = (hashCode * 397) ^ this.R.GetHashCode();
                hashCode = (hashCode * 397) ^ this.G.GetHashCode();
                hashCode = (hashCode * 397) ^ this.B.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Checks the equality of the current color and the given color.
        /// </summary>
        /// <param name="other">
        /// The other color to compare.
        /// </param>
        /// <returns>
        /// Return true if the colors are equal.
        /// </returns>
        protected bool Equals(Color other)
        {
            return this.A == other.A && this.R == other.R && this.G == other.G && this.B == other.B;
        }
    }
}