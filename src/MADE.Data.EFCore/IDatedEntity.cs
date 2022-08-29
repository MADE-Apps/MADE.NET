// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore
{
    using System;

    /// <summary>
    /// Defines a base definition for an entity with defined created and updated date.
    /// </summary>
    public interface IDatedEntity
    {
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