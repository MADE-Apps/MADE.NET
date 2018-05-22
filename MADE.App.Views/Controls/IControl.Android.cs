#if __ANDROID__
namespace MADE.App.Views.Controls
{
    using System.ComponentModel;

    using Android.Util;

    /// <summary>
    /// Defines an interface for Android components of a common application control.
    /// </summary>
    public partial interface IControl : IView, INotifyPropertyChanged
    {
        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        /// <param name="attrs">
        /// The XML attributes set.
        /// </param>
        /// <param name="defStyleAttr">
        /// The XML default style attribute.
        /// </param>
        /// <param name="defStyleRes">
        /// The default style resource.
        /// </param>
        void OnApplyTemplate(IAttributeSet attrs, int defStyleAttr, int defStyleRes);
    }
}
#endif