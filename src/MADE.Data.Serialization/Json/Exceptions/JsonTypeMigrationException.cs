// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Serialization.Json.Exceptions
{
    using System;

    /// <summary>
    /// Defines an exception for errors occurred when interacting with JSON type migrations.
    /// </summary>
    public class JsonTypeMigrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationException"/> class.
        /// </summary>
        public JsonTypeMigrationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationException"/> class with a message that describes the error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public JsonTypeMigrationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationException"/> class with a message that describes the error and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception to be thrown.</param>
        public JsonTypeMigrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}