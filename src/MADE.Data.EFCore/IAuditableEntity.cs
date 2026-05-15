// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore;

/// <summary>
/// Defines an interface for entities that track who created and last updated them.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the entity.
    /// </summary>
    string? UpdatedBy { get; set; }
}
