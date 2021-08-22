// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using MADE.UI;

    /// <summary>
    /// Defines an interface for components of a common application control.
    /// </summary>
    public interface IControl : IView
    {
        /// <summary>
        /// Retrieves the element from the instantiated view by the given resource identifier.
        /// </summary>
        /// <param name="resourceName">
        /// The name of the resource to find.
        /// </param>
        /// <typeparam name="TView">
        /// The type of view to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the view from the layout, if the view is found.
        /// </returns>
        TView GetChildView<TView>(string resourceName)
            where TView : class;
    }
}