namespace MADE.UnitTests.Common.Tests.Data.Validation.Rules
{
    using System.Globalization;

    using MADE.Data.Validation.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DecimalValidationRuleTests
    {
        [TestMethod]
        public void IsValid_AcceptsDecimalString()
        {
            string obj = decimal.One.ToString(CultureInfo.CurrentCulture);

            DecimalValidationRule rule = new DecimalValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsDecimalObject()
        {
            decimal obj = decimal.One;

            DecimalValidationRule rule = new DecimalValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsEmptyString()
        {
            string obj = string.Empty;

            DecimalValidationRule rule = new DecimalValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptNullObject()
        {
            object obj = null;

            DecimalValidationRule rule = new DecimalValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptRandomString()
        {
            string obj = "Hello, World!";

            DecimalValidationRule rule = new DecimalValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsZeroDecimal()
        {
            decimal obj = decimal.Zero;

            DecimalValidationRule rule =
                new DecimalValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsPositiveDecimal()
        {
            decimal obj = decimal.One;

            DecimalValidationRule rule =
                new DecimalValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_DoesNotAcceptNegativeDecimal()
        {
            decimal obj = decimal.MinusOne;

            DecimalValidationRule rule =
                new DecimalValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsZeroDecimal()
        {
            decimal obj = decimal.Zero;

            DecimalValidationRule rule =
                new DecimalValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsNegativeDecimal()
        {
            decimal obj = decimal.MinusOne;

            DecimalValidationRule rule =
                new DecimalValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_DoesNotAcceptPositiveDecimal()
        {
            decimal obj = decimal.One;

            DecimalValidationRule rule =
                new DecimalValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsFalse(rule.IsValid(obj));
        }
    }
}