using UIKit;

namespace MADE.UI.Design.Styling
{
    public class UILabelStyle : StyleBase<UILabel>
    {
        public Color ForegroundColor { get; set; }

        public UITextAlignment TextAlignment { get; set; }

        public override void Apply(UILabel view)
        {
            base.Apply(view);

            if (ForegroundColor != null)
            {
                view.TextColor = ForegroundColor;
            }

            view.TextAlignment = TextAlignment;
        }
    }
}