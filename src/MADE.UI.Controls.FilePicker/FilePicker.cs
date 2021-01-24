// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System;
    using System.Collections.Generic;
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
    public class FilePicker : Control, IFilePicker
    {
        private const string FilePickerChooseFileButtonPart = "FilePickerChooseFileButton";

        private const string FilePickerItemsViewPart = "FilePickerItemsView";

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
        /// Initializes a new instance of the <see cref="FilePicker"/> class.
        /// </summary>
        public FilePicker()
        {
            this.DefaultStyleKey = typeof(FilePicker);
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

            base.OnApplyTemplate();

            this.ChooseFileButton = this.GetChildView<Button>(FilePickerChooseFileButtonPart);
            this.ItemsView = this.GetChildView<ListViewBase>(FilePickerItemsViewPart);

            if (this.ChooseFileButton != null)
            {
                this.ChooseFileButton.Click += this.OnChooseFileButtonClick;
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FilePickerAutomationPeer(this);
        }

        private async void OnChooseFileButtonClick(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker {ViewMode = PickerViewMode.Thumbnail};
            fileOpenPicker.FileTypeFilter.Add(".jpg");

            var selectedFiles = new List<StorageFile>();
            if (this.SelectionMode == FilePickerSelectionMode.Single)
            {
                StorageFile file = await fileOpenPicker.PickSingleFileAsync();
                if (file != null)
                {
                    selectedFiles.Add(file);
                }
            }
            else
            {
                IReadOnlyList<StorageFile> files = await fileOpenPicker.PickMultipleFilesAsync();
                if (files != null)
                {
                    selectedFiles.AddRange(files);
                }
            }
        }
    }
}