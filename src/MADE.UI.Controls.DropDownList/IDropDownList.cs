// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines an interface for a selection control that provides a drop-down list box that allows users to select one or multiple items from a list.
    /// </summary>
    public interface IDropDownList
    {
        /// <summary>
        /// Occurs when the drop-down opens.
        /// </summary>
        event EventHandler<object> DropDownOpened;

        /// <summary>
        /// Occurs when the drop-down closes.
        /// </summary>
        event EventHandler<object> DropDownClosed;

        /// <summary>
        /// Occurs when the currently selected item changes.
        /// </summary>
        event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Gets or sets the content for the control's header.
        /// </summary>
        object Header { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content of the control's header.
        /// </summary>
        DataTemplate HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the content to display on the collapsed drop down.
        /// </summary>
        object Content { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content on the collapsed drop down.
        /// </summary>
        DataTemplate ContentTemplate { get; set; }

        /// <summary>
        /// Gets or sets a reference to a custom DataTemplateSelector logic class.
        /// <para>
        /// The DataTemplateSelector referenced by this property returns a template to apply to the content on the collapsed drop down.
        /// </para>
        /// </summary>
        DataTemplateSelector ContentTemplateSelector { get; set; }

        /// <summary>
        /// Gets or sets an object source used to generate the content of the control.
        /// </summary>
        object ItemsSource { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used to display each item.
        /// </summary>
        DataTemplate ItemTemplate { get; set; }

        /// <summary>
        /// Gets or sets a reference to a custom DataTemplateSelector logic class.
        /// <para>
        /// The DataTemplateSelector referenced by this property returns a template to apply to items.
        /// </para>
        /// </summary>
        DataTemplateSelector ItemTemplateSelector { get; set; }

        /// <summary>
        /// Gets or sets the template that defines the panel that controls the layout of items.
        /// </summary>
        ItemsPanelTemplate ItemsPanel { get; set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        object SelectedItem { get; set; }

        /// <summary>
        /// Gets the currently selected items.
        /// </summary>
        IList<object> SelectedItems { get; }

        /// <summary>
        /// Gets or sets the selection behavior.
        /// </summary>
        DropDownListSelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the drop-down is currently open.
        /// </summary>
        bool IsDropDownOpen { get; set; }

        /// <summary>
        /// Gets or sets the maximum height for the drop-down.
        /// </summary>
        double MaxDropDownHeight { get; set; }
    }
}
