// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore;

/// <summary>
/// Defines an interface for entities that support soft deletion.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been deleted.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the date the entity was deleted.
    /// </summary>
    DateTime? DeletedDate { get; set; }
}
