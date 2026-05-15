// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation;

/// <summary>
/// Defines an interface for a data validator that performs asynchronous validation.
/// </summary>
public interface IAsyncValidator
{
    /// <summary>
    /// Gets or sets the key associated with the validator.
    /// </summary>
    string Key { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the data provided is in an invalid state.
    /// </summary>
    bool IsInvalid { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the data is dirty.
    /// </summary>
    bool IsDirty { get; set; }

    /// <summary>
    /// Gets or sets the feedback message to display when <see cref="IsInvalid"/> is true.
    /// </summary>
    string FeedbackMessage { get; set; }

    /// <summary>
    /// Executes data validation on the provided <paramref name="value"/> asynchronously.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An asynchronous operation.</returns>
    Task ValidateAsync(object value, CancellationToken cancellationToken = default);
}
