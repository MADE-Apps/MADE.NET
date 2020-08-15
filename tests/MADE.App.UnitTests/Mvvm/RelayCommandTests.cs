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
    public class RelayCommandTests
    {
        [TestMethod]
        public void WhenCallingCanExecute_ShouldReturnTrue()
        {
            // Arrange
            var command = new RelayCommand(() => { });

            // Act
            bool canExecute = command.CanExecute();

            // Assert
            canExecute.ShouldBeTrue();
        }

        [TestMethod]
        public void WhenCallingExecute_ShouldRunAction()
        {
            // Arrange
            var autoResetEvent = new AutoResetEvent(false);

            var command = new RelayCommand(
                () =>
                {
                    autoResetEvent.Set();
                });

            // Act
            command.Execute();

            // Assert
            autoResetEvent.WaitOne().ShouldBeTrue();
        }
    }
}