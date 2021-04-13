namespace MADE.Data.Converters.Exceptions
{
    using System;

    /// <summary>
    /// Defines an exception thrown when a data conversion has failed.
    /// </summary>
    public class InvalidDataConversionException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidDataConversionException" /> class.</summary>
        public InvalidDataConversionException(string converter, object value)
        {
            this.Converter = converter;
            this.Value = value;
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidDataConversionException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidDataConversionException(string converter, object value, string message)
            : base(message)
        {
            this.Converter = converter;
            this.Value = value;
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidDataConversionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InvalidDataConversionException(string converter, object value, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Converter = converter;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name of the converter that failed to convert.
        /// </summary>
        public string Converter { get; }

        /// <summary>
        /// Gets the value that failed to convert.
        /// </summary>
        public object Value { get; }
    }
}