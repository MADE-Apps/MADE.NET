#if __IOS__
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.NSObject.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for NSObject.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE
{
    using System;
    using System.Runtime.InteropServices;

    using Foundation;

    using MADE.Enums;

    /// <summary>
    /// Defines a collection of extensions for NSObject.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Associates the given value with the given <see cref="NSObject"/> by the given key.
        /// </summary>
        /// <param name="nsObject">
        /// The object to associated the value with.
        /// </param>
        /// <param name="key">
        /// The key for the associated value.
        /// </param>
        /// <param name="value">
        /// The value to associate.
        /// </param>
        public static void SetAssociatedObject(this NSObject nsObject, NSString key, NSObject value)
        {
            objc_setAssociatedObject(
                nsObject.Handle,
                key.Handle,
                value?.Handle ?? IntPtr.Zero,
                AssociationPolicy.Retain);
        }

        /// <summary>
        /// Gets the associated value from the given <see cref="NSObject"/> by the given key.
        /// </summary>
        /// <param name="nsObject">
        /// The object to retrieve the associated value from.
        /// </param>
        /// <param name="key">
        /// The key to retrieve a value for.
        /// </param>
        /// <returns>
        /// Returns the associated value.
        /// </returns>
        public static object GetAssociatedObject(this NSObject nsObject, NSString key)
        {
            IntPtr associatedObjectHandle = objc_getAssociatedObject(nsObject.Handle, key.Handle);
            NSObject associatedObject = ObjCRuntime.Runtime.GetNSObject(associatedObjectHandle);

            return associatedObject;
        }

        [DllImport("/usr/lib/libobjc.dylib")]
        private static extern void objc_setAssociatedObject(
            IntPtr obj,
            IntPtr key,
            IntPtr value,
            AssociationPolicy policy);

        [DllImport("/usr/lib/libobjc.dylib")]
        private static extern IntPtr objc_getAssociatedObject(IntPtr obj, IntPtr key);
    }
}
#endif