#if __IOS__
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssociationPolicy.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines values to specify the behavior of an association.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Enums
{
    /// <summary>
    /// Defines values to specify the behavior of an association.
    /// </summary>
    public enum AssociationPolicy
    {
        /// <summary>
        /// Specifies a weak reference to the associated object.
        /// </summary>
        Assign = 0,

        /// <summary>
        /// Specifies a strong reference to the associated object, and that the association is not made atomically.
        /// </summary>
        RetainNonAtomic = 1,

        /// <summary>
        /// Specifies that the associated object is copied, and that the association is not made atomically.
        /// </summary>
        CopyNonAtomic = 3,

        /// <summary>
        /// Specifies a strong reference to the associated object, and that the association is made atomically.
        /// </summary>
        Retain = 01401,

        /// <summary>
        /// Specifies that the associated object is copied, and that the association is made atomically.
        /// </summary>
        Copy = 01403
    }
}
#endif