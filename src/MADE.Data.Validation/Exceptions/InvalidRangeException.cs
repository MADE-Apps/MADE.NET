// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Exceptions
{
    using System;

    /// <summary>
    /// Defines an exception for an invalid range.
    /// </summary>
    public class InvalidRangeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRangeException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public InvalidRangeException(string message)
            : base(message)
        {
        }
    }
}
