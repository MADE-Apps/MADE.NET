namespace MADE.Media.Image.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Xaml.Interactivity;
    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Defines a behavior for loading a storage file's thumbnail image into a <see cref="Image"/> control.
    /// </summary>
    public class LoadStorageFileThumbnailImageBehavior : Behavior<Image>
    {
        /// <summary>
        /// Identifies the <see cref="File"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FileProperty = DependencyProperty.Register(
            nameof(File),
            typeof(StorageFile),
            typeof(LoadStorageFileThumbnailImageBehavior),
            new PropertyMetadata(
                null,
                async (d, e) =>
                    await ((LoadStorageFileThumbnailImageBehavior)d).UpdateImageSourceAsync((StorageFile)e.NewValue)));

        private IReadOnlyList<string> supportedImageTypes;

        /// <summary>
        /// Gets or sets the storage file to retrieve a thumbnail for.
        /// </summary>
        public StorageFile File
        {
            get => (StorageFile)this.GetValue(FileProperty);
            set => this.SetValue(FileProperty, value);
        }

        private IReadOnlyList<string> SupportedImageTypes => this.supportedImageTypes ??= new List<string> { ".jpg", ".png", ".jpeg", ".tiff", ".bmp" };

        private async Task UpdateImageSourceAsync(StorageFile storageFile)
        {
            if (storageFile == null)
            {
                return;
            }

            string fileType = storageFile.FileType;

            ThumbnailMode thumbnailMode = ThumbnailMode.SingleItem;

            if (this.SupportedImageTypes.Contains(fileType))
            {
                thumbnailMode = ThumbnailMode.PicturesView;
            }

            StorageItemThumbnail thumbnailStorageItem = await storageFile.GetThumbnailAsync(
                thumbnailMode,
                64,
                ThumbnailOptions.ResizeThumbnail);

            if (thumbnailStorageItem == null)
            {
                return;
            }

            var bitmapImage = new BitmapImage();
            bitmapImage.SetSource(thumbnailStorageItem.CloneStream());

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.Source = bitmapImage;
            }
        }
    }
}
