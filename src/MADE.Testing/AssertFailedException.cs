// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing;

/// <summary>
/// Defines an exception that is thrown when an assertion fails.
/// </summary>
public class AssertFailedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssertFailedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the assertion failure.</param>
    public AssertFailedException(string message)
        : base(message)
    {
    }
}
