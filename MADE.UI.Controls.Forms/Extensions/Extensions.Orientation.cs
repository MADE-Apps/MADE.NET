// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Orientation.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for orientation support in Xamarin.Forms controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls.Forms
{
    using MADE.UI.Layout;

    using Xamarin.Forms;

    /// <summary>
    /// Defines a collection of extensions for orientation support in Xamarin.Forms controls.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts the given internal orientation enum to the Xamarin.Forms <see cref="StackOrientation"/> enum.
        /// </summary>
        /// <param name="orientation">
        /// The orientation to convert.
        /// </param>
        /// <returns>
        /// Returns the equivalent <see cref="StackOrientation"/> value.
        /// </returns>
        public static StackOrientation ToStackOrientation(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Horizontal:
                    return StackOrientation.Horizontal;
            }

            return StackOrientation.Vertical;
        }

        /// <summary>
        /// Converts the given Xamarin.Forms <see cref="StackOrientation"/> enum to the internal orientation enum.
        /// </summary>
        /// <param name="orientation">
        /// The orientation to convert.
        /// </param>
        /// <returns>
        /// Returns the equivalent <see cref="Orientation"/> value.
        /// </returns>
        public static Orientation ToOrientation(this StackOrientation orientation)
        {
            switch (orientation)
            {
                case StackOrientation.Horizontal:
                    return Orientation.Horizontal;
            }

            return Orientation.Vertical;
        }
    }
}