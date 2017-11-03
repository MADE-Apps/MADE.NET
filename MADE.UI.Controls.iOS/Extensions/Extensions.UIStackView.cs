using MADE.UI.Layout;
using UIKit;

namespace MADE.UI.Controls
{
    public static partial class Extensions
    {
        public static void SetOrientation(this UIStackView layout, Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Axis = orientation == Orientation.Vertical ? UILayoutConstraintAxis.Vertical : UILayoutConstraintAxis.Horizontal;
        }
    }
}