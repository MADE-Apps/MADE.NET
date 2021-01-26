namespace MADE.UI.Controls
{
    using Windows.UI.Xaml;

    public class FilePickerItemClickEventArgs : RoutedEventArgs
    {
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
