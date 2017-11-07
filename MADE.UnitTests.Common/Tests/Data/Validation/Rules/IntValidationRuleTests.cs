namespace MADE.UnitTests.Common.Tests.Data.Validation.Rules
{
    using System.Globalization;

    using MADE.Data.Validation.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IntValidationRuleTests
    {
        [TestMethod]
        public void IsValid_AcceptsIntString()
        {
            string obj = int.MaxValue.ToString(CultureInfo.CurrentCulture);

            IntValidationRule rule = new IntValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsIntObject()
        {
            int obj = int.MaxValue;

            IntValidationRule rule = new IntValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_AcceptsEmptyString()
        {
            string obj = string.Empty;

            IntValidationRule rule = new IntValidationRule();

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptNullObject()
        {
            object obj = null;

            IntValidationRule rule = new IntValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptRandomString()
        {
            string obj = "Hello, World!";

            IntValidationRule rule = new IntValidationRule();

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsZeroInt()
        {
            int obj = 0;

            IntValidationRule rule =
                new IntValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_AcceptsPositiveInt()
        {
            int obj = int.MaxValue;

            IntValidationRule rule =
                new IntValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void PositiveNumericValidationRule_IsValid_DoesNotAcceptNegativeInt()
        {
            int obj = int.MinValue;

            IntValidationRule rule =
                new IntValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Positive };

            Assert.IsFalse(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsZeroInt()
        {
            int obj = 0;

            IntValidationRule rule =
                new IntValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_AcceptsNegativeInt()
        {
            int obj = int.MinValue;

            IntValidationRule rule =
                new IntValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsTrue(rule.IsValid(obj));
        }

        [TestMethod]
        public void NegativeNumericValidationRule_IsValid_DoesNotAcceptPositiveInt()
        {
            int obj = int.MaxValue;

            IntValidationRule rule =
                new IntValidationRule { NumericValidationSetting = NumericValidationRuleSetting.Negative };

            Assert.IsFalse(rule.IsValid(obj));
        }
    }
}