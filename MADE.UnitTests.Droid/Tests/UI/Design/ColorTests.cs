namespace MADE.UnitTests.Droid.Tests.UI.Design
{
    using System.Drawing;

    using Android.Provider;

    using NUnit.Framework;

    [TestFixture]
    public class ColorTests
    {
        public void Ctor_SystemColorInitializes()
        {
            var systemColor = Color.Red;

            var actualColor = new MADE.UI.Design.Color(Color.Red);

            Assert.AreEqual(systemColor.A, actualColor.A);
            Assert.AreEqual(systemColor.R, actualColor.R);
            Assert.AreEqual(systemColor.G, actualColor.G);
            Assert.AreEqual(systemColor.B, actualColor.B);
        }

        public void Ctor_AndroidColorInitializes()
        {
            var androidColor = Android.Graphics.Color.Red;

            var actualColor = new MADE.UI.Design.Color(Android.Graphics.Color.Red);

            Assert.AreEqual(androidColor.A, actualColor.A);
            Assert.AreEqual(androidColor.R, actualColor.R);
            Assert.AreEqual(androidColor.G, actualColor.G);
            Assert.AreEqual(androidColor.B, actualColor.B);
        }

        public void EqualsOperator_ColorDoesNotEqualNull()
        {
            var color1 = new MADE.UI.Design.Color(Android.Graphics.Color.Red);
            MADE.UI.Design.Color color2 = null;

            Assert.IsFalse(color1 == color2);
        }

        public void EqualsOperator_SameColorsEqual()
        {
            var color1 = new MADE.UI.Design.Color(Android.Graphics.Color.Red);
            var color2 = new MADE.UI.Design.Color(Android.Graphics.Color.Red);

            Assert.IsTrue(color1 == color2);
        }
    }
}