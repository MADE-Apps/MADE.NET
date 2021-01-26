// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Media.Image.Behaviors
{
    using System;
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

        /// <summary>
        /// Gets or sets the storage file to retrieve a thumbnail for.
        /// </summary>
        public StorageFile File
        {
            get => (StorageFile)this.GetValue(FileProperty);
            set => this.SetValue(FileProperty, value);
        }

        private async Task UpdateImageSourceAsync(IStorageItemProperties file)
        {
            if (file == null)
            {
                return;
            }

            StorageItemThumbnail thumbnail = await file.GetThumbnailAsync(
                ThumbnailMode.SingleItem,
                256,
                ThumbnailOptions.ResizeThumbnail);

            if (thumbnail == null)
            {
                return;
            }

            if (this.AssociatedObject != null)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(thumbnail.CloneStream());

                this.AssociatedObject.Source = bitmapImage;
            }
        }
    }
}
