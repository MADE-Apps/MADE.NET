// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation
{
    using System;

    /// <summary>
    /// Defines an event argument for an input validated request.
    /// </summary>
    public class InputValidatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidatedEventArgs"/> class.
        /// </summary>
        /// <param name="isInvalid">
        /// A value indicating whether the input is invalid.
        /// </param>
        public InputValidatedEventArgs(bool isInvalid)
            : this(isInvalid, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidatedEventArgs"/> class.
        /// </summary>
        /// <param name="isInvalid">
        /// A value indicating whether the input is invalid.
        /// </param>
        /// <param name="isDirty">
        /// A value indicating whether the input is dirty.
        /// </param>
        public InputValidatedEventArgs(bool isInvalid, bool isDirty)
        {
            this.IsInvalid = isInvalid;
            this.IsDirty = isDirty;
        }

        /// <summary>
        /// Gets a value indicating whether the input is dirty.
        /// </summary>
        public bool IsDirty { get; }

        /// <summary>
        /// Gets a value indicating whether the input is invalid.
        /// </summary>
        public bool IsInvalid { get; }
    }
}