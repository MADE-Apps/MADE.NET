// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using MADE.Data.Validation.Extensions;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Automation.Peers;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines an input control that provides a user with the ability to select files.
    /// </summary>
    [TemplatePart(Name = FilePickerChooseFileButtonPart, Type = typeof(Button))]
    [TemplatePart(Name = FilePickerItemsViewPart, Type = typeof(ListViewBase))]
    [TemplatePart(Name = FilePickerHeaderPresenterPart, Type = typeof(ContentPresenter))]
    public partial class FilePicker : Control, IFilePicker
    {
        /// <summary>
        /// Identifies the <see cref="Header"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(FilePicker),
            new PropertyMetadata(null, (o, args) => ((FilePicker)o).SetHeaderVisibility()));

        /// <summary>
        /// Identifies the <see cref="HeaderTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            nameof(HeaderTemplate),
            typeof(DataTemplate),
            typeof(FilePicker),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="ChooseFileButtonContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ChooseFileButtonContentProperty = DependencyProperty.Register(
            nameof(ChooseFileButtonContent),
            typeof(object),
            typeof(FilePicker),
            new PropertyMetadata("Choose file"));

        /// <summary>
        /// Identifies the <see cref="ChooseFileButtonContentTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ChooseFileButtonContentTemplateProperty = DependencyProperty.Register(
            nameof(ChooseFileButtonContentTemplate),
            typeof(DataTemplate),
            typeof(FilePicker),
            new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Identifies the <see cref="SelectionMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(FilePickerSelectionMode),
            typeof(FilePicker),
            new PropertyMetadata(default(FilePickerSelectionMode)));

        /// <summary>
        /// Identifies the <see cref="FileTypes"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FileTypesProperty = DependencyProperty.Register(
            nameof(FileTypes),
            typeof(IEnumerable),
            typeof(FilePicker),
            new PropertyMetadata(new List<string> { "*" }));

        /// <summary>
        /// Identifies the <see cref="AppendFiles"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AppendFilesProperty = DependencyProperty.Register(
            nameof(AppendFiles),
            typeof(bool),
            typeof(FilePicker),
            new PropertyMetadata(default(bool)));

        /// <summary>
        /// Identifies the <see cref="Files"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FilesProperty = DependencyProperty.Register(
            nameof(Files),
            typeof(IList),
            typeof(FilePicker),
            new PropertyMetadata(new ObservableCollection<FilePickerItem>()));

        /// <summary>
        /// Identifies the <see cref="ItemsViewStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsViewStyleProperty = DependencyProperty.Register(
            nameof(ItemsViewStyle),
            typeof(Style),
            typeof(FilePicker),
            new PropertyMetadata(default(Style)));

        private const string FilePickerChooseFileButtonPart = "FilePickerChooseFileButton";

        private const string FilePickerItemsViewPart = "FilePickerItemsView";

        private const string FilePickerHeaderPresenterPart = "FilePickerHeaderPresenter";

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePicker"/> class.
        /// </summary>
        public FilePicker()
        {
            this.DefaultStyleKey = typeof(FilePicker);
        }

        /// <summary>
        /// Occurs when an item in the list receives an interaction.
        /// </summary>
        public event FilePickerItemClickEventHandler ItemClick;

        /// <summary>
        /// Gets or sets the data used for the header of the control.
        /// </summary>
        public object Header
        {
            get => this.GetValue(HeaderProperty);
            set => this.SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the template used to display the content of the control's header.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)this.GetValue(HeaderTemplateProperty);
            set => this.SetValue(HeaderTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the content of the choose file button.</summary>
        public object ChooseFileButtonContent
        {
            get => (object)this.GetValue(ChooseFileButtonContentProperty);
            set => this.SetValue(ChooseFileButtonContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template that is used to display the content of the choose file button.
        /// </summary>
        public DataTemplate ChooseFileButtonContentTemplate
        {
            get => (DataTemplate)this.GetValue(ChooseFileButtonContentTemplateProperty);
            set => this.SetValue(ChooseFileButtonContentTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the file picker selection mode.
        /// </summary>
        public FilePickerSelectionMode SelectionMode
        {
            get => (FilePickerSelectionMode)this.GetValue(SelectionModeProperty);
            set => this.SetValue(SelectionModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the file types supported by the input.
        /// </summary>
        public IEnumerable FileTypes
        {
            get => (IEnumerable)this.GetValue(FileTypesProperty);
            set => this.SetValue(FileTypesProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to append files with subsequent file choices.
        /// </summary>
        public bool AppendFiles
        {
            get => (bool)this.GetValue(AppendFilesProperty);
            set => this.SetValue(AppendFilesProperty, value);
        }

        /// <summary>
        /// Gets or sets the files chosen.
        /// </summary>
        public IList Files
        {
            get => (IList)this.GetValue(FilesProperty);
            set => this.SetValue(FilesProperty, value);
        }

        /// <summary>
        /// Gets or sets the style of the items view.
        /// </summary>
        public Style ItemsViewStyle
        {
            get => (Style)this.GetValue(ItemsViewStyleProperty);
            set => this.SetValue(ItemsViewStyleProperty, value);
        }

        /// <summary>
        /// Gets the view representing the button that opens the file picker.
        /// </summary>
        public Button ChooseFileButton { get; private set; }

        /// <summary>
        /// Gets the view representing the items that have been picked.
        /// </summary>
        public ListViewBase ItemsView { get; private set; }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (this.ChooseFileButton != null)
            {
                this.ChooseFileButton.Click -= this.OnChooseFileButtonClick;
            }

            if (this.ItemsView != null)
            {
                this.ItemsView.ItemClick -= this.OnItemsViewItemClick;
            }

            base.OnApplyTemplate();

            this.ChooseFileButton = this.GetChildView<Button>(FilePickerChooseFileButtonPart);
            this.ItemsView = this.GetChildView<ListViewBase>(FilePickerItemsViewPart);

            if (this.ChooseFileButton != null)
            {
                this.ChooseFileButton.Click += this.OnChooseFileButtonClick;
            }

            if (this.ItemsView != null)
            {
                this.ItemsView.ItemClick += this.OnItemsViewItemClick;
            }
        }

        /// <summary>
        /// Provides the class-specific <see cref="FilePickerAutomationPeer"/> implementation for the Microsoft UI Automation infrastructure.
        /// </summary>
        /// <returns>The class-specific <see cref="FilePickerAutomationPeer"/> instance.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FilePickerAutomationPeer(this);
        }

        private static async Task<FilePickerItem> CreateFilePickerItemAsync(StorageFile file)
        {
            var filePickerItem = new FilePickerItem { File = file };
            await filePickerItem.LoadThumbnailAsync();
            return filePickerItem;
        }

        private void SetHeaderVisibility()
        {
            if (!(this.GetTemplateChild(FilePickerHeaderPresenterPart) is FrameworkElement headerPresenter))
            {
                return;
            }

            if (this.Header is string headerText)
            {
                headerPresenter.Visibility = headerText.IsNullOrWhiteSpace()
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
            else
            {
                headerPresenter.Visibility = this.Header != null
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void OnItemsViewItemClick(object sender, ItemClickEventArgs e)
        {
            if (e?.ClickedItem == null)
            {
                return;
            }

            if (this.CanRemoveFilePickerItem(e.ClickedItem as FilePickerItem))
            {
                this.Files.Remove(e.ClickedItem);
            }
        }

        private bool CanRemoveFilePickerItem(FilePickerItem item)
        {
            if (item == null)
            {
                return false;
            }

            bool canRemove = true;

            FilePickerItemClickEventHandler itemClick = this.ItemClick;
            if (itemClick == null)
            {
                return true;
            }

            var args = new FilePickerItemClickEventArgs(item);
            foreach (Delegate d in itemClick.GetInvocationList())
            {
                var handler = (FilePickerItemClickEventHandler)d;
                handler(this, args);
                if (args.CancelRemove)
                {
                    canRemove = false;
                }
            }

            return canRemove;
        }

        private async void OnChooseFileButtonClick(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker { ViewMode = PickerViewMode.Thumbnail };

            if (this.FileTypes != null)
            {
                foreach (object fileType in this.FileTypes)
                {
                    fileOpenPicker.FileTypeFilter.Add(fileType.ToString());
                }
            }

            if (this.SelectionMode == FilePickerSelectionMode.Single)
            {
                StorageFile file = await fileOpenPicker.PickSingleFileAsync();
                if (file == null || this.Files == null)
                {
                    return;
                }

                if (!this.AppendFiles)
                {
                    this.Files.Clear();
                }

                await this.AddFileAsync(file);
            }
            else
            {
                IReadOnlyList<StorageFile> files = await fileOpenPicker.PickMultipleFilesAsync();
                if (files == null || this.Files == null)
                {
                    return;
                }

                if (!this.AppendFiles)
                {
                    this.Files.Clear();
                }

                foreach (StorageFile file in files)
                {
                    await this.AddFileAsync(file);
                }
            }
        }

        private async Task AddFileAsync(StorageFile file)
        {
            FilePickerItem existingFile = this.Files.Cast<FilePickerItem>().FirstOrDefault(item => item.Path == file.Path);
            if (existingFile != null)
            {
                return;
            }

            FilePickerItem filePickerItem = await CreateFilePickerItemAsync(file);
            this.Files.Add(filePickerItem);
        }
    }
}