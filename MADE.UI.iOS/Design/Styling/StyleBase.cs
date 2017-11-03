using UIKit;

namespace MADE.UI.Design.Styling
{
    public abstract class StyleBase<TView> where TView : UIView
    {
        public float? Opacity { get; set; }

        public Color BackgroundColor { get; set; }

        public virtual void Apply(TView view)
        {
            if (view != null)
            {
                if (BackgroundColor != null)
                {
                    view.BackgroundColor = BackgroundColor;
                }

                if (Opacity != null && Opacity >= 0.0 && Opacity <= 1.0)
                {
                    view.Alpha = (System.nfloat)Opacity;
                }
            }
        }
    }
}