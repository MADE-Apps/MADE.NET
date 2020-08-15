namespace MADE.App.UnitTests.Mvvm
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using App.Mvvm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [TestCategory("Mvvm")]
    [ExcludeFromCodeCoverage]
    public class BindableTests
    {
        [TestMethod]
        public void WhenSettingPropertyWithDifferentValue_ShouldUpdateValue()
        {
            // Arrange
            var test = new BindableExample();

            test.IsActive.ShouldBeFalse();

            // Act
            test.IsActive = true;

            // Assert
            test.IsActive.ShouldBeTrue();
        }

        [TestMethod]
        public void WhenSettingPropertyWithDifferentValue_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            var receivedEvents = new List<string>();

            var test = new BindableExample();

            test.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
                {
                    receivedEvents.Add(e.PropertyName);
                };

            test.IsActive.ShouldBeFalse();

            // Act
            test.IsActive = true;

            // Assert
            receivedEvents.Count.ShouldBe(1);
            receivedEvents[0].ShouldBe(nameof(BindableExample.IsActive));
        }

        [TestMethod]
        public void WhenCallingRaisePropertyChanged_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            var receivedEvents = new List<string>();

            var test = new BindableExample();

            test.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
                {
                    receivedEvents.Add(e.PropertyName);
                };

            // Act
            test.RaisePropertyChanged(() => test.IsActive);

            // Assert
            receivedEvents.Count.ShouldBe(1);
            receivedEvents[0].ShouldBe(nameof(BindableExample.IsActive));
        }

        private class BindableExample : Bindable
        {
            private bool isActive;

            public bool IsActive
            {
                get => this.isActive;
                set => this.Set(() => this.IsActive, ref this.isActive, value);
            }
        }
    }
}