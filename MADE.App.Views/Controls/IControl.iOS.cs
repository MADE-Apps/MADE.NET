#if __IOS__
namespace MADE.App.Views.Controls
{
    using System.ComponentModel;

    using UIKit;

    public partial interface IControl : IView, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the name of the nib to load.
        /// </summary>
        string NibName { get; }

        /// <summary>
        /// Gets the view associated with the root of the control.
        /// </summary>
        UIView Root { get; }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        void OnApplyTemplate();
    }
}
#endif