// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore
{
    using System;

    /// <summary>
    /// Defines a base definition for an entity.
    /// </summary>
    public interface IEntityBase
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the entity's creation.
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date of the entity's last update.
        /// </summary>
        DateTime? UpdatedDate { get; set; }
    }
}