namespace MADE.UnitTests.Common.Tests.Data.Validation.Rules
{
    using System;
    using System.Globalization;

    using MADE.Data.Validation.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeValidationRuleTests
    {
        [TestMethod]
        public void IsValid_DoesNotAcceptNullObject()
        {
            object nullObject = null;

            DateTimeValidationRule rule = new DateTimeValidationRule();

            Assert.IsFalse(rule.IsValid(nullObject));
        }

        [TestMethod]
        public void IsValid_AcceptsEmptyString()
        {
            string emptyString = string.Empty;

            DateTimeValidationRule rule = new DateTimeValidationRule();

            Assert.IsTrue(rule.IsValid(emptyString));
        }

        [TestMethod]
        public void IsValid_AcceptsDateTimeObject()
        {
            DateTime dateTime = DateTime.Now;

            DateTimeValidationRule rule = new DateTimeValidationRule();

            Assert.IsTrue(rule.IsValid(dateTime));
        }

        [TestMethod]
        public void IsValid_AcceptsDateString()
        {
            string dateTimeString = DateTime.Now.ToString(CultureInfo.CurrentCulture);

            DateTimeValidationRule rule = new DateTimeValidationRule();

            Assert.IsTrue(rule.IsValid(dateTimeString));
        }

        [TestMethod]
        public void IsValid_DoesNotAcceptRandomString()
        {
            string randomString = "Hello, World!";

            DateTimeValidationRule rule = new DateTimeValidationRule();

            Assert.IsFalse(rule.IsValid(randomString));
        }
    }
}