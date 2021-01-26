namespace MADE.UI.Controls
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Defines a wrapper for an item chosen by the file picker.
    /// </summary>
    public class FilePickerItem
    {
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        public StorageFile File { get; set; }

        /// <summary>
        /// Gets the thumbnail for the file.
        /// </summary>
        public BitmapImage Thumbnail { get; private set; }

        /// <summary>
        /// Gets the name of the file including the file name extension.
        /// </summary>
        public string Name => this.File?.Name;

        /// <summary>
        /// Gets a user-friendly name for the file.
        /// </summary>
        public string DisplayName => this.File?.DisplayName;

        /// <summary>
        /// Gets the type (file name extension) of the file.
        /// </summary>
        public string FileType => this.File?.FileType;

        /// <summary>
        /// Gets the full file-system path of the current file, if the file has a path.
        /// </summary>
        public string Path => this.File?.Path;

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        internal async Task LoadThumbnailAsync()
        {
            if (this.File != null)
            {
                StorageItemThumbnail thumbnail =
                    await this.File.GetThumbnailAsync(ThumbnailMode.SingleItem, 256, ThumbnailOptions.ResizeThumbnail);

                if (thumbnail == null)
                {
                    return;
                }

                this.Thumbnail = new BitmapImage();
                this.Thumbnail.SetSource(thumbnail.CloneStream());
            }
        }
    }
}