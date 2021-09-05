namespace MADE.Foundation.Platform
{
    using System;

    /// <summary>
    /// Defines an exception thrown when code is called for a platform that is not supported.
    /// </summary>
    public class PlatformNotSupportedException : NotImplementedException
    {
        /// <summary>Initializes a new instance of the <see cref="PlatformNotSupportedException" /> class with default properties.</summary>
        public PlatformNotSupportedException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PlatformNotSupportedException" /> class with a specified error message.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public PlatformNotSupportedException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PlatformNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
        public PlatformNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}