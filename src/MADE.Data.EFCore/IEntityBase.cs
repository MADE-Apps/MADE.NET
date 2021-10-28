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