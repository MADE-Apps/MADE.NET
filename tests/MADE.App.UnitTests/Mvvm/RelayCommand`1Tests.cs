namespace MADE.App.UnitTests.Mvvm
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using App.Mvvm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [TestCategory("Mvvm")]
    [ExcludeFromCodeCoverage]
    public class GenericRelayCommandTests
    {
        [TestMethod]
        public void WhenCallingCanExecuteWithValidParameter_ShouldReturnTrue()
        {
            // Arrange
            var command = new RelayCommand<bool>(
                b =>
                    {
                    });

            // Act
            bool canExecute = command.CanExecute(true);

            // Assert
            canExecute.ShouldBeTrue();
        }

        [TestMethod]
        public void WhenCallingCanExecuteWithInvalidParameter_ShouldReturnFalse()
        {
            // Arrange
            var command = new RelayCommand<bool>(
                b =>
                    {
                    });

            // Act
            bool canExecute = command.CanExecute("Hello, World!");

            // Assert
            canExecute.ShouldBeFalse();
        }

        [TestMethod]
        public void WhenCallingExecuteWithValidParameter_ShouldRunAction()
        {
            // Arrange
            var autoResetEvent = new AutoResetEvent(false);

            var command = new RelayCommand<bool>(
                b =>
                    {
                        autoResetEvent.Set();
                    });

            // Act
            command.Execute(true);

            // Assert
            autoResetEvent.WaitOne().ShouldBeTrue();
        }

        [TestMethod]
        public void WhenCallingExecuteWithInvalidParameter_ShouldNotRunAction()
        {
            // Arrange
            var autoResetEvent = new AutoResetEvent(false);

            var command = new RelayCommand<bool>(
                b =>
                    {
                        autoResetEvent.Set();
                    });

            // Act
            command.Execute("Hello, World!");

            // Assert
            autoResetEvent.WaitOne(1000).ShouldBeFalse();
        }
    }
}
