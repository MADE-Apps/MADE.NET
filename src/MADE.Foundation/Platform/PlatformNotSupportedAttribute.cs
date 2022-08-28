// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Foundation.Platform
{
    using System;

    /// <summary>
    /// Defines an attribute that marks a component as not supported by a specific platform.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class PlatformNotSupportedAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformNotSupportedAttribute"/> class.
        /// </summary>
        public PlatformNotSupportedAttribute()
        {
        }
    }
}