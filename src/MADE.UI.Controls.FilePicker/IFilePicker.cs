// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System.Collections;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines an interface for an input control that provides a user with the ability to select files.
    /// </summary>
    public interface IFilePicker
    {
        /// <summary>
        /// Occurs when an item in the list receives an interaction.
        /// </summary>
        event FilePickerItemClickEventHandler ItemClick;

        /// <summary>
        /// Gets or sets the data used for the header of each control.
        /// </summary>
        object Header { get; set; }

        /// <summary>
        /// Gets or sets the template used to display the content of the control's header.
        /// </summary>
        DataTemplate HeaderTemplate { get; set; }

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

        /// <summary>
        /// Gets or sets the file types supported by the input.
        /// </summary>
        IEnumerable FileTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to append files with subsequent file choices.
        /// </summary>
        bool AppendFiles { get; set; }

        /// <summary>
        /// Gets or sets the files chosen.
        /// </summary>
        IList Files { get; set; }

        /// <summary>
        /// Gets or sets the style of the items view.
        /// </summary>
        Style ItemsViewStyle { get; set; }
    }
}
