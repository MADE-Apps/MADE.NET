// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines an event argument for when a file picker item is clicked from the list.
    /// </summary>
    public class FilePickerItemClickEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePickerItemClickEventArgs"/> class with the reference to the clicked item.
        /// </summary>
        /// <param name="clickedItem">The reference to the clicked item.</param>
        public FilePickerItemClickEventArgs(object clickedItem)
        {
            this.ClickedItem = clickedItem;
        }

        /// <summary>Gets a reference to the clicked item.</summary>
        public object ClickedItem { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to cancel the removal of the clicked item.
        /// </summary>
        public bool CancelRemove { get; set; }
    }
}
