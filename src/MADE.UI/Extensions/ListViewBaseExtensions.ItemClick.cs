namespace MADE.UI.Extensions
{
    using System.Windows.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines an extension to the <see cref="ListViewBase"/> control for binding a command to the item click event.
    /// </summary>
    public static class ListViewBaseExtensions
    {
        /// <summary>
        /// Defines the dependency property for the item click command.
        /// </summary>
        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.RegisterAttached(
                "ItemClickCommand",
                typeof(ICommand),
                typeof(ListViewBaseExtensions),
                new PropertyMetadata(
                    null,
                    (o, args) =>
                    {
                        if (args.NewValue != null)
                        {
                            RegisterItemClickEvent(o as ListViewBase);
                        }
                    }));

        /// <summary>
        /// Sets the item click command for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> to set the command to.
        /// </param>
        /// <param name="command">
        /// The <see cref="ICommand"/> to set.
        /// </param>
        public static void SetItemClickCommand(DependencyObject obj, ICommand command)
        {
            obj.SetValue(ItemClickCommandProperty, command);
        }

        /// <summary>
        /// Gets the item click command for the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> to get the command from.
        /// </param>
        /// <returns>
        /// Returns the <see cref="ICommand"/>.
        /// </returns>
        public static ICommand GetItemClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ItemClickCommandProperty);
        }

        private static void RegisterItemClickEvent(ListViewBase listViewBase)
        {
            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ItemClick -= OnItemClicked;
            listViewBase.ItemClick += OnItemClicked;
        }

        private static void OnItemClicked(object sender, ItemClickEventArgs e)
        {
            if (!(sender is ListViewBase listViewBase))
            {
                return;
            }

            // If in single selection mode, this will deselect any selected item and reset selection mode to none.
            if (listViewBase.SelectionMode == ListViewSelectionMode.Single)
            {
                listViewBase.SelectedItem = null;
                listViewBase.SelectionMode = ListViewSelectionMode.None;
            }

            ICommand itemClickCommand = GetItemClickCommand(listViewBase);
            if (itemClickCommand != null && itemClickCommand.CanExecute(e.ClickedItem))
            {
                itemClickCommand.Execute(e.ClickedItem);
            }
        }
    }
}