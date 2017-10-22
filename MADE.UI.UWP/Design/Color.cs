// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Color.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a UI element color.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Design
{
    /// <summary>
    /// Defines a model for a UI element color.
    /// </summary>
    public class Color : IColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
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
        public Color(System.Drawing.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color);
        }

        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
#endif

#if __ANDROID__
        public Color(Android.Graphics.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public static implicit operator Color(Android.Graphics.Color color)
        {
            return new Color(color);
        }

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
            this.A = (byte)(color.CIColor.Alpha * 255);
            this.R = (byte)(color.CIColor.Red * 255);
            this.G = (byte)(color.CIColor.Green * 255);
            this.B = (byte)(color.CIColor.Blue * 255);
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
            return !color1.Equals(color2);
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