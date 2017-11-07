namespace MADE.UnitTests.Common.Tests.Common.Extensions
{
    using System;
    using System.Globalization;

    using MADE.Common;
    using MADE.Common.Dates;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void GetFirstDateOfWeek_ReturnsExpectedDayOfWeek()
        {
            DateTimeFormatInfo format = CultureInfo.CurrentCulture.DateTimeFormat;
            DayOfWeek expectedFirstDay = format.FirstDayOfWeek;

            DateTime date = DateTime.Now;

            Assert.AreEqual(expectedFirstDay, date.GetFirstDateOfWeek().DayOfWeek);
        }

        [TestMethod]
        public void GetCurrentAgeInYears_ReturnsExpectedAge()
        {
            int expectedAge = 10;
            var expectedAgeAsDays = expectedAge * 366; // Using 366 to counter the DateTime.Now time component.

            DateTime ageDate = DateTime.Now.Subtract(TimeSpan.FromDays(expectedAgeAsDays));

            Assert.AreEqual(expectedAge, ageDate.GetCurrentAgeInYears());
        }

        [TestMethod]
        public void GetDayState_TimeAfter05ReturnsMorning()
        {
            DayState expectedState = DayState.Morning;

            DateTime dateTime = new DateTime(2017, 11, 7, 05, 0, 0);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore05DoesNotReturnMorning()
        {
            DayState expectedState = DayState.Morning;

            DateTime dateTime = new DateTime(2017, 11, 7, 04, 59, 59);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore12ReturnsMorning()
        {
            DayState expectedState = DayState.Morning;

            DateTime dateTime = new DateTime(2017, 11, 7, 11, 59, 59);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter12DoesNotReturnMorning()
        {
            DayState expectedState = DayState.Morning;

            DateTime dateTime = new DateTime(2017, 11, 7, 12, 00, 00);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter12ReturnsAfternoon()
        {
            DayState expectedState = DayState.Afternoon;

            DateTime dateTime = new DateTime(2017, 11, 7, 12, 0, 0);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore12DoesNotReturnAfternoon()
        {
            DayState expectedState = DayState.Afternoon;

            DateTime dateTime = new DateTime(2017, 11, 7, 11, 59, 59);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore17ReturnsAfternoon()
        {
            DayState expectedState = DayState.Afternoon;

            DateTime dateTime = new DateTime(2017, 11, 7, 16, 59, 59);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter17DoesNotReturnAfternoon()
        {
            DayState expectedState = DayState.Afternoon;

            DateTime dateTime = new DateTime(2017, 11, 7, 17, 00, 00);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter17ReturnsEvening()
        {
            DayState expectedState = DayState.Evening;

            DateTime dateTime = new DateTime(2017, 11, 7, 17, 0, 0);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore17DoesNotReturnEvening()
        {
            DayState expectedState = DayState.Evening;

            DateTime dateTime = new DateTime(2017, 11, 7, 16, 59, 59);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore20ReturnsEvening()
        {
            DayState expectedState = DayState.Evening;

            DateTime dateTime = new DateTime(2017, 11, 7, 19, 59, 59);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter20DoesNotReturnEvening()
        {
            DayState expectedState = DayState.Evening;

            DateTime dateTime = new DateTime(2017, 11, 7, 20, 00, 00);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter20ReturnsNight()
        {
            DayState expectedState = DayState.Night;

            DateTime dateTime = new DateTime(2017, 11, 7, 20, 0, 0);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore20DoesNotReturnNight()
        {
            DayState expectedState = DayState.Night;

            DateTime dateTime = new DateTime(2017, 11, 7, 19, 59, 59);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeBefore05ReturnsNight()
        {
            DayState expectedState = DayState.Night;

            DateTime dateTime = new DateTime(2017, 11, 7, 04, 59, 59);

            Assert.AreEqual(expectedState, dateTime.GetDayState());
        }

        [TestMethod]
        public void GetDayState_TimeAfter05DoesNotReturnNight()
        {
            DayState expectedState = DayState.Night;

            DateTime dateTime = new DateTime(2017, 11, 7, 05, 00, 00);

            Assert.AreNotEqual(expectedState, dateTime.GetDayState());
        }
    }
}