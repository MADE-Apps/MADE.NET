#if WINDOWS_UWP
namespace MADE.App.Views
{
    using MADE.App.Design.Color;

    public partial interface IView
    {
        /// <summary>
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        Color BackgroundColor { get; set; }
    }
}
#endif