// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Thickness.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a UI thickness for padding or margins.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Layout
{
    /// <summary>
    /// Defines a model for a UI thickness for padding or margins.
    /// </summary>
    public class Thickness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> class.
        /// </summary>
        /// <param name="uniformLengthPixels">
        /// A single length value to apply to all parts of the thickness in pixels.
        /// </param>
        public Thickness(double uniformLengthPixels)
        {
            this.Bottom = uniformLengthPixels;
            this.Left = uniformLengthPixels;
            this.Right = uniformLengthPixels;
            this.Top = uniformLengthPixels;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> class.
        /// </summary>
        /// <param name="leftPx">
        /// The left value.
        /// </param>
        /// <param name="topPx">
        /// The top value.
        /// </param>
        /// <param name="rightPx">
        /// The right value.
        /// </param>
        /// <param name="bottomPx">
        /// The bottom value.
        /// </param>
        public Thickness(double leftPx, double topPx, double rightPx, double bottomPx)
        {
            this.Bottom = bottomPx;
            this.Left = leftPx;
            this.Right = rightPx;
            this.Top = topPx;
        }

#if WINDOWS_UWP
        public Thickness(Windows.UI.Xaml.Thickness thickness)
        {
            this.Bottom = thickness.Bottom;
            this.Left = thickness.Left;
            this.Right = thickness.Right;
            this.Top = thickness.Top;
        }

        public static implicit operator Thickness(Windows.UI.Xaml.Thickness thickness)
        {
            return new Thickness(thickness);
        }

        public static implicit operator Windows.UI.Xaml.Thickness(Thickness thickness)
        {
            return new Windows.UI.Xaml.Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
        }
#endif

        /// <summary>
        /// Gets or sets the bottom value.
        /// </summary>
        public double Bottom { get; set; }

        /// <summary>
        /// Gets or sets the left value.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets the right value.
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// Gets or sets the top value.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Checks the equality of two thickness items.
        /// </summary>
        /// <param name="t1">
        /// The thickness 1.
        /// </param>
        /// <param name="t2">
        /// The thickness 2.
        /// </param>
        /// <returns>
        /// Returns true if the thickness values are equal.
        /// </returns>
        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return t1.Equals(t2);
        }

        /// <summary>
        /// Checks the inequality of two thickness items.
        /// </summary>
        /// <param name="t1">
        /// The thickness 1.
        /// </param>
        /// <param name="t2">
        /// The thickness 2.
        /// </param>
        /// <returns>
        /// Returns true if the thickness values are not equal.
        /// </returns>
        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return !t1.Equals(t2);
        }

#if __ANDROID__
        /// <summary>
        /// Converts the thickness value to the correct density pixels for the device.
        /// </summary>
        /// <returns>
        /// Returns the thickness converted to density pixels.
        /// </returns>
        public Thickness InDensityPixels()
        {
            float d = Android.App.Application.Context.Resources.DisplayMetrics.Density;
            return new Thickness(this.Left / d, this.Top / d, this.Right / d, this.Bottom / d);
        }
#endif

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

            return obj.GetType() == this.GetType() && this.Equals((Thickness)obj);
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
                int hashCode = this.Bottom.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Left.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Right.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Top.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Checks the equality of the current thickness and the given thickness.
        /// </summary>
        /// <param name="other">
        /// The other thickness to compare.
        /// </param>
        /// <returns>
        /// Return true if the thickness values are equal.
        /// </returns>
        protected bool Equals(Thickness other)
        {
            return this.Bottom.Equals(other.Bottom) && this.Left.Equals(other.Left) && this.Right.Equals(other.Right) && this.Top.Equals(other.Top);
        }
    }
}