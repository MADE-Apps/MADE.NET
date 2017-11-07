namespace MADE.UnitTests.Common.Tests.Data.Validation.Rules
{
    using System;

    using MADE.Data.Validation.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeRangeValidationRuleTests
    {
        [TestMethod]
        public void DefaultCtor_MinDateIsDateTimeMinValue()
        {
            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule();
            Assert.AreEqual(DateTime.MinValue, rule.MinDate);
        }

        [TestMethod]
        public void DefaultCtor_MaxDateIsDateTimeMaxValue()
        {
            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule();
            Assert.AreEqual(DateTime.MaxValue, rule.MaxDate);
        }

        [TestMethod]
        public void OptionalCtor_MinDateIsSet()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.AreEqual(minDate, rule.MinDate);
        }

        [TestMethod]
        public void OptionalCtor_MaxDateIsSet()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.AreEqual(maxDate, rule.MaxDate);
        }

        [TestMethod]
        public void IsValid_DefaultCtorAcceptsMinDate()
        {
            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule();
            Assert.IsTrue(rule.IsValid(DateTime.MinValue));
        }

        [TestMethod]
        public void IsValid_DefaultCtorAcceptsMaxDate()
        {
            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule();
            Assert.IsTrue(rule.IsValid(DateTime.MaxValue));
        }

        [TestMethod]
        public void IsValid_OptionalCtorAcceptsMinDate()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.IsTrue(rule.IsValid(minDate));
        }

        [TestMethod]
        public void IsValid_OptionalCtorAcceptsMaxDate()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.IsTrue(rule.IsValid(maxDate));
        }

        [TestMethod]
        public void IsValid_OptionalCtorAcceptsDateAboveMinDate()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.IsTrue(rule.IsValid(minDate.Add(TimeSpan.FromDays(1))));
        }

        [TestMethod]
        public void IsValid_OptionalCtorAcceptsDateBelowMaxDate()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.IsTrue(rule.IsValid(maxDate.Subtract(TimeSpan.FromDays(1))));
        }

        [TestMethod]
        public void IsValid_OptionalCtorDoesNotAcceptsDateBelowMinDate()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.IsFalse(rule.IsValid(minDate.Subtract(TimeSpan.FromDays(1))));
        }

        [TestMethod]
        public void IsValid_OptionalCtorDoesNotAcceptsDateAboveMaxDate()
        {
            DateTime minDate = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            DateTime maxDate = DateTime.Now;

            DateTimeRangeValidationRule rule = new DateTimeRangeValidationRule(minDate, maxDate);
            Assert.IsFalse(rule.IsValid(maxDate.Add(TimeSpan.FromDays(1))));
        }
    }
}