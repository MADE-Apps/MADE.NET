namespace MADE.UI.Controls.Forms
{
    using MADE.UI.Layout;

    using Xamarin.Forms;

    public static partial class Extensions
    {
        public static StackOrientation ToStackOrientation(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Horizontal:
                    return StackOrientation.Horizontal;
            }

            return StackOrientation.Vertical;
        }
    }
}