// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColor.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a color component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Design
{
    /// <summary>
    /// Defines an interface for a color component.
    /// </summary>
    public interface IColor
    {
        /// <summary>
        /// Gets or sets the alpha component of the color.
        /// </summary>
        byte A { get; set; }

        /// <summary>
        /// Gets or sets the red component of the color.
        /// </summary>
        byte R { get; set; }

        /// <summary>
        /// Gets or sets the green component of the color.
        /// </summary>
        byte G { get; set; }

        /// <summary>
        /// Gets or sets the blue component of the color.
        /// </summary>
        byte B { get; set; }
    }
}