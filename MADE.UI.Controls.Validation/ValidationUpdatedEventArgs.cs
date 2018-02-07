// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationUpdatedEventArgs.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines event arguments for a control that updated validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;

    /// <summary>
    /// Defines event arguments for a control that updated validation.
    /// </summary>
    public class ValidationUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationUpdatedEventArgs"/> class.
        /// </summary>
        /// <param name="isValid">
        /// A value indicating whether the associated sender is valid.
        /// </param>
        public ValidationUpdatedEventArgs(bool isValid)
        {
            this.IsValid = isValid;
        }

        /// <summary>
        /// Gets a value indicating whether the associated sender is valid.
        /// </summary>
        public bool IsValid { get; }
    }
}