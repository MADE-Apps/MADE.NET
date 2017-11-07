namespace MADE.UnitTests.Droid.Tests.UI.Design
{
    using System.Drawing;

    using NUnit.Framework;

    [TestFixture]
    public class ColorTests
    {
        public void Ctor_SystemColorInitializes()
        {
            Color systemColor = Color.Red;

            MADE.UI.Design.Color actualColor = new MADE.UI.Design.Color(Color.Red);

            Assert.AreEqual(systemColor.A, actualColor.A);
            Assert.AreEqual(systemColor.R, actualColor.R);
            Assert.AreEqual(systemColor.G, actualColor.G);
            Assert.AreEqual(systemColor.B, actualColor.B);
        }

        public void Ctor_AndroidColorInitializes()
        {
            Android.Graphics.Color androidColor = Android.Graphics.Color.Red;

            MADE.UI.Design.Color actualColor = new MADE.UI.Design.Color(Android.Graphics.Color.Red);

            Assert.AreEqual(androidColor.A, actualColor.A);
            Assert.AreEqual(androidColor.R, actualColor.R);
            Assert.AreEqual(androidColor.G, actualColor.G);
            Assert.AreEqual(androidColor.B, actualColor.B);
        }

        public void EqualsOperator_ColorDoesNotEqualNull()
        {
            MADE.UI.Design.Color color1 = new MADE.UI.Design.Color(Android.Graphics.Color.Red);
            MADE.UI.Design.Color color2 = null;

            Assert.IsFalse(color1 == color2);
        }

        public void EqualsOperator_SameColorsEqual()
        {
            MADE.UI.Design.Color color1 = new MADE.UI.Design.Color(Android.Graphics.Color.Red);
            MADE.UI.Design.Color color2 = new MADE.UI.Design.Color(Android.Graphics.Color.Red);

            Assert.IsTrue(color1 == color2);
        }
    }
}