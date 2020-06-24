namespace MADE.App.Mvvm.UnitTests
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using MADE.App.Mvvm.UnitTests.Stubs;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BindableTests
    {
        [TestMethod]
        public void Bindable_Set_DifferentValueWillSet()
        {
            TestBindableClass bindable = new TestBindableClass();

            Assert.AreEqual(bindable.BindableBool, false);

            bindable.BindableBool = true;

            Assert.AreEqual(bindable.BindableBool, true);
        }

        [TestMethod]
        public void Bindable_Set_DifferentValueWillRaisePropertyChanged()
        {
            List<string> receivedEvents = new List<string>();

            TestBindableClass bindable = new TestBindableClass();

            bindable.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                {
                    receivedEvents.Add(e.PropertyName);
                };

            Assert.AreEqual(bindable.BindableBool, false);

            bindable.BindableBool = true;
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual(nameof(TestBindableClass.BindableBool), receivedEvents[0]);
        }

        [TestMethod]
        public void Bindable_RaisePropertyChanged_CallingRaisesPropertyChanged()
        {
            List<string> receivedEvents = new List<string>();

            TestBindableClass bindable = new TestBindableClass();

            bindable.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                {
                    receivedEvents.Add(e.PropertyName);
                };

            bindable.RaisePropertyChanged(() => bindable.BindableBool);

            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual(nameof(TestBindableClass.BindableBool), receivedEvents[0]);
        }
    }
}