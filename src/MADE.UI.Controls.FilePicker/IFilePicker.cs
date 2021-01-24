// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines an interface for an input control that provides a user with the ability to select files.
    /// </summary>
    public interface IFilePicker
    {
        /// <summary>
        /// Gets or sets the content of the choose file button.</summary>
        object ChooseFileButtonContent { get; set; }

        /// <summary>
        /// Gets or sets the data template that is used to display the content of the choose file button.
        /// </summary>
        DataTemplate ChooseFileButtonContentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the file picker selection mode.
        /// </summary>
        FilePickerSelectionMode SelectionMode { get; set; }
    }
}
