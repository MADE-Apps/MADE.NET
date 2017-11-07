namespace MADE.UnitTests.Common.Tests.Data.Validation.Rules
{
    using System.Globalization;

    using MADE.Data.Validation.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FloatValidationRuleTests
    {
        [TestMethod]
        public void IsValid_AcceptsFloatString()
        {
            string obj = float.Epsilon.ToString(CultureInfo.CurrentCulture);

            FloatValidationRule rule = new FloatValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsFloatObject()
        {
            float obj = float.Epsilon;

            FloatValidationRule rule = new FloatValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsEmptyString()
        {
            string obj = string.Empty;

            FloatValidationRule rule = new FloatValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptNullObject()
        {
            object obj = null;

            FloatValidationRule rule = new FloatValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptRandomString()
        {
            string obj = "Hello, World!";

            FloatValidationRule rule = new FloatValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsZeroFloat()
        {
            float obj = 0;

            FloatValidationRule rule =
                new FloatValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsPositiveFloat()
        {
            float obj = float.Epsilon;

            FloatValidationRule rule =
                new FloatValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_DoesNotAcceptNegativeFloat()
        {
            float obj = -float.Epsilon;

            FloatValidationRule rule =
                new FloatValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsZeroFloat()
        {
            float obj = 0;

            FloatValidationRule rule =
                new FloatValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsNegativeFloat()
        {
            float obj = -float.Epsilon;

            FloatValidationRule rule =
                new FloatValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_DoesNotAcceptPositiveFloat()
        {
            float obj = float.Epsilon;

            FloatValidationRule rule =
                new FloatValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsFalse(rule.IsValid(obj));
        }
    }
}