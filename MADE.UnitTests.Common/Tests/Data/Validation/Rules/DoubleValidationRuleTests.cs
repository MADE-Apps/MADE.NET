namespace MADE.UnitTests.Common.Tests.Data.Validation.Rules
{
    using System.Globalization;

    using MADE.Data.Validation.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DoubleValidationRuleTests
    {
        [TestMethod]
        public void IsValid_AcceptsDoubleString()
        {
            string obj = double.Epsilon.ToString(CultureInfo.CurrentCulture);

            DoubleValidationRule rule = new DoubleValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsDoubleObject()
        {
            double obj = double.Epsilon;

            DoubleValidationRule rule = new DoubleValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsEmptyString()
        {
            string obj = string.Empty;

            DoubleValidationRule rule = new DoubleValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptNullObject()
        {
            object obj = null;

            DoubleValidationRule rule = new DoubleValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptRandomString()
        {
            string obj = "Hello, World!";

            DoubleValidationRule rule = new DoubleValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsZeroDouble()
        {
            double obj = 0;

            DoubleValidationRule rule =
                new DoubleValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsPositiveDouble()
        {
            double obj = double.Epsilon;

            DoubleValidationRule rule =
                new DoubleValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_DoesNotAcceptNegativeDouble()
        {
            double obj = -double.Epsilon;

            DoubleValidationRule rule =
                new DoubleValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsZeroDouble()
        {
            double obj = 0;

            DoubleValidationRule rule =
                new DoubleValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsNegativeDouble()
        {
            double obj = -double.Epsilon;

            DoubleValidationRule rule =
                new DoubleValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_DoesNotAcceptPositiveDouble()
        {
            double obj = double.Epsilon;

            DoubleValidationRule rule =
                new DoubleValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsFalse(rule.IsValid(obj));
        }
    }
}