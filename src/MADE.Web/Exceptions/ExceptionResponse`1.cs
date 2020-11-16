// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Exceptions
{
    using System;

    /// <summary>
    /// Defines a response to a an exception being thrown.
    /// </summary>
    /// <typeparam name="TException">The type of exception thrown.</typeparam>
    public class ExceptionResponse<TException>
        where TException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionResponse{TException}" /> class with an error code and message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="exception">The exception thrown.</param>
        public ExceptionResponse(string errorCode, string errorMessage, TException exception)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception thrown.
        /// </summary>
        public TException Exception { get; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; }
    }
}