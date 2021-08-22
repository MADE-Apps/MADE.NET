// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using MADE.Collections;
    using MADE.Data.Validation.Extensions;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Automation.Peers;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines a selection control that provides a drop-down list box that allows users to select one or multiple items from a list.
    /// </summary>
    [TemplatePart(Name = DropDownButtonPart, Type = typeof(Button))]
    [TemplatePart(Name = DropDownPart, Type = typeof(Popup))]
    [TemplatePart(Name = DropDownBorderPart, Type = typeof(Border))]
    [TemplatePart(Name = DropDownContentPart, Type = typeof(ListView))]
    public class DropDownList : Control, IDropDownList
    {
        /// <summary>
        /// Identifies the <see cref="Header"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(DropDownList),
            new PropertyMetadata(default));

        /// <summary>
        /// Identifies the <see cref="HeaderTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            nameof(HeaderTemplate),
            typeof(DataTemplate),
            typeof(DropDownList),
            new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Identifies the <see cref="Content"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(DropDownList),
            new PropertyMetadata(default));

        /// <summary>
        /// Identifies the <see cref="ContentTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(
            nameof(ContentTemplate),
            typeof(DataTemplate),
            typeof(DropDownList),
            new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Identifies the <see cref="ContentTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(ContentTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(DropDownList),
            new PropertyMetadata(default(DataTemplateSelector)));

        /// <summary>
        /// Identifies the <see cref="ItemsSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(object),
            typeof(DropDownList),
            new PropertyMetadata(default));

        /// <summary>
        /// Identifies the <see cref="ItemTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(DropDownList),
            new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Identifies the <see cref="ItemTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateSelectorProperty = DependencyProperty.Register(
            nameof(ItemTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(DropDownList),
            new PropertyMetadata(default(DataTemplateSelector)));

        /// <summary>
        /// Identifies the <see cref="ItemsPanel"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsPanelProperty = DependencyProperty.Register(
            nameof(ItemsPanel),
            typeof(ItemsPanelTemplate),
            typeof(DropDownList),
            new PropertyMetadata(default(ItemsPanelTemplate)));

        /// <summary>
        /// Identifies the <see cref="SelectedItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(DropDownList),
            new PropertyMetadata(default, (o, args) => ((DropDownList)o).OnSelectedItemChanged()));

        /// <summary>
        /// Identifies the <see cref="SelectionMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(DropDownListSelectionMode),
            typeof(DropDownList),
            new PropertyMetadata(default(DropDownListSelectionMode), (o, args) => ((DropDownList)o).SetSelectionMode()));

        /// <summary>
        /// Identifies the <see cref="IsDropDownOpen"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            nameof(IsDropDownOpen),
            typeof(bool),
            typeof(DropDownList),
            new PropertyMetadata(default(bool)));

        /// <summary>
        /// Identifies the <see cref="MaxDropDownHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register(
            nameof(MaxDropDownHeight),
            typeof(double),
            typeof(DropDownList),
            new PropertyMetadata(248D));

        private const string DropDownButtonPart = "DropDownButton";

        private const string DropDownPart = "DropDown";

        private const string DropDownBorderPart = "DropDownBorder";

        private const string DropDownContentPart = "DropDownContent";

        private readonly List<object> selectedItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownList"/> class.
        /// </summary>
        public DropDownList()
        {
            this.DefaultStyleKey = typeof(DropDownList);
            this.selectedItems = new List<object>();
            this.SizeChanged += this.OnSizeChanged;
        }

        /// <summary>
        /// Occurs when the drop-down opens.
        /// </summary>
        public event EventHandler<object> DropDownOpened;

        /// <summary>
        /// Occurs when the drop-down closes.
        /// </summary>
        public event EventHandler<object> DropDownClosed;

        /// <summary>
        /// Occurs when the currently selected item changes.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Gets or sets the content for the control's header.
        /// </summary>
        public object Header
        {
            get => this.GetValue(HeaderProperty);
            set => this.SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content of the control's header.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)this.GetValue(HeaderTemplateProperty);
            set => this.SetValue(HeaderTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the content to display on the collapsed drop down.
        /// </summary>
        public object Content
        {
            get => this.GetValue(ContentProperty);
            set => this.SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content on the collapsed drop down.
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)this.GetValue(ContentTemplateProperty);
            set => this.SetValue(ContentTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets a reference to a custom DataTemplateSelector logic class.
        /// <para>
        /// The DataTemplateSelector referenced by this property returns a template to apply to the content on the collapsed drop down.
        /// </para>
        /// </summary>
        public DataTemplateSelector ContentTemplateSelector
        {
            get => (DataTemplateSelector)this.GetValue(ContentTemplateSelectorProperty);
            set => this.SetValue(ContentTemplateSelectorProperty, value);
        }

        /// <summary>
        /// Gets or sets an object source used to generate the content of the control.
        /// </summary>
        public object ItemsSource
        {
            get => this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the DataTemplate used to display each item.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets a reference to a custom DataTemplateSelector logic class.
        /// <para>
        /// The DataTemplateSelector referenced by this property returns a template to apply to items.
        /// </para>
        /// </summary>
        public DataTemplateSelector ItemTemplateSelector
        {
            get => (DataTemplateSelector)this.GetValue(ItemTemplateSelectorProperty);
            set => this.SetValue(ItemTemplateSelectorProperty, value);
        }

        /// <summary>
        /// Gets or sets the template that defines the panel that controls the layout of items.
        /// </summary>
        public ItemsPanelTemplate ItemsPanel
        {
            get => (ItemsPanelTemplate)this.GetValue(ItemsPanelProperty);
            set => this.SetValue(ItemsPanelProperty, value);
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Gets the currently selected items.
        /// </summary>
        public IList<object> SelectedItems => this.selectedItems;

        /// <summary>
        /// Gets or sets the selection behavior.
        /// </summary>
        public DropDownListSelectionMode SelectionMode
        {
            get => (DropDownListSelectionMode)this.GetValue(SelectionModeProperty);
            set => this.SetValue(SelectionModeProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the drop-down is currently open.
        /// </summary>
        public bool IsDropDownOpen
        {
            get => (bool)this.GetValue(IsDropDownOpenProperty);
            set => this.SetValue(IsDropDownOpenProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum height for the drop-down.
        /// </summary>
        public double MaxDropDownHeight
        {
            get => (double)this.GetValue(MaxDropDownHeightProperty);
            set => this.SetValue(MaxDropDownHeightProperty, value);
        }

        /// <summary>
        /// Gets the view representing the button that opens the drop down.
        /// </summary>
        public Button DropDownButton { get; private set; }

        /// <summary>
        /// Gets the view representing the drop down.
        /// </summary>
        public Popup DropDown { get; private set; }

        /// <summary>
        /// Gets the view representing the drop down content's border.
        /// </summary>
        public Border DropDownBorder { get; private set; }

        /// <summary>
        /// Gets the view representing the items that appear in the drop down.
        /// </summary>
        public ListView DropDownContent { get; private set; }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (this.DropDownButton != null)
            {
                this.DropDownButton.Click -= this.OnDropDownButtonClick;
            }

            if (this.DropDown != null)
            {
                this.DropDown.Opened -= this.OnDropDownOpened;
                this.DropDown.Closed -= this.OnDropDownClosed;
            }

            if (this.DropDownContent != null)
            {
                this.DropDownContent.SelectionChanged -= this.OnDropDownContentSelectionChanged;
            }

            base.OnApplyTemplate();

            this.DropDownButton = this.GetChildView<Button>(DropDownButtonPart);
            this.DropDown = this.GetChildView<Popup>(DropDownPart);
            this.DropDownBorder = this.GetChildView<Border>(DropDownBorderPart);
            this.DropDownContent = this.GetChildView<ListView>(DropDownContentPart);

            if (this.DropDownButton != null)
            {
                this.DropDownButton.Click += this.OnDropDownButtonClick;
            }

            if (this.DropDown != null)
            {
                this.DropDown.Opened += this.OnDropDownOpened;
                this.DropDown.Closed += this.OnDropDownClosed;
            }

            if (this.DropDownContent != null)
            {
                this.DropDownContent.SelectionChanged += this.OnDropDownContentSelectionChanged;
            }

            this.SetSelectionMode();
            this.DetermineDropDownButtonContent();
        }

        /// <summary>
        /// Provides the class-specific <see cref="DropDownListAutomationPeer"/> implementation for the Microsoft UI Automation infrastructure.
        /// </summary>
        /// <returns>The class-specific <see cref="DropDownListAutomationPeer"/> instance.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new DropDownListAutomationPeer(this);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ResizeDropDownBorder();
        }

        private void OnDropDownButtonClick(object sender, RoutedEventArgs e)
        {
            this.ResizeDropDownBorder();

            if (this.DropDown == null)
            {
                return;
            }

            this.DropDown.IsOpen = true;
            this.DropDownContent?.Focus(FocusState.Programmatic);
        }

        private void OnDropDownOpened(object sender, object e)
        {
            this.DropDownOpened?.Invoke(this, e);

            if (!(Window.Current.Content is Frame frame) || this.DropDownButton == null)
            {
                return;
            }

            GeneralTransform positionTransform = this.TransformToVisual(Window.Current.Content);
            Point position = positionTransform.TransformPoint(new Point(0, 0));

            // Gets the actual height of the control & the popup.
            double popupHeight = this.DropDownButton.ActualHeight;
            if (this.DropDown.Child is FrameworkElement element)
            {
                popupHeight += element.ActualHeight.IsCloseTo(double.MinValue) || element.ActualHeight.IsCloseTo(0)
                    ? this.MaxDropDownHeight
                    : element.ActualHeight;
            }
            else
            {
                popupHeight += this.MaxDropDownHeight;
            }

            double verticalOffset = frame.ActualHeight - (position.Y + popupHeight);
            this.DropDown.VerticalOffset = verticalOffset <= 0 ? verticalOffset : 0.0;
        }

        private void OnSelectedItemChanged()
        {
            if (this.DropDownContent != null)
            {
                this.DropDownContent.SelectedItem = this.SelectedItem;
            }
        }

        private void OnDropDownClosed(object sender, object e)
        {
            this.DropDownClosed?.Invoke(this, e);
        }

        private void OnDropDownContentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectionChanged?.Invoke(this, e);

            if (this.SelectionMode == DropDownListSelectionMode.Single && this.DropDown != null)
            {
                this.DropDown.IsOpen = false;
            }

            this.DetermineDropDownButtonContent();
        }

        private void DetermineDropDownButtonContent()
        {
            if (this.DropDownContent != null)
            {
                if (this.SelectionMode == DropDownListSelectionMode.Single)
                {
                    this.SelectedItem = this.DropDownContent.SelectedItem;
                    this.SelectedItems.Clear();
                }
                else
                {
                    this.SelectedItem = null;
                    this.SelectedItems.MakeEqualTo(this.DropDownContent.SelectedItems);
                }
            }

            IList<object> result = new List<object>();

            switch (this.SelectionMode)
            {
                case DropDownListSelectionMode.Single when this.SelectedItem != null:
                    result.Add(this.SelectedItem);
                    break;
                case DropDownListSelectionMode.Multiple when this.SelectedItems != null:
                    result = this.SelectedItems;
                    break;
            }

            this.Content = string.Join(", ", result);
        }

        private void ResizeDropDownBorder()
        {
            if (this.DropDownBorder != null)
            {
                this.DropDownBorder.Width = this.ActualWidth;
            }
        }

        private void SetSelectionMode()
        {
            if (this.DropDownContent != null)
            {
                this.DropDownContent.SelectionMode = this.SelectionMode == DropDownListSelectionMode.Multiple
                    ? ListViewSelectionMode.Multiple
                    : ListViewSelectionMode.Single;
            }
        }
    }
}