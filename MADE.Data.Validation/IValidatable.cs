// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidatable.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for an object that can be validated.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation
{
    /// <summary>
    /// Defines an interface for an object that can be validated.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Checks whether the current object is valid.
        /// </summary>
        /// <returns>
        /// Returns true if the object is valid.
        /// </returns>
        bool IsValid();
    }
}
