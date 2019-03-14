namespace MADE.App.Mvvm.UnitTests
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RelayCommand_1Tests
    {
        [TestMethod]
        public void RelayCommand1_CanExecute_ValidParameterShouldPass()
        {
            RelayCommand<bool> command = new RelayCommand<bool>(
                b =>
                    {
                    });

            Assert.IsTrue(command.CanExecute(true));
        }

        [TestMethod]
        public void RelayCommand1_CanExecute_InvalidParameterShouldFail()
        {
            RelayCommand<bool> command = new RelayCommand<bool>(
                b =>
                    {
                    });

            Assert.IsFalse(command.CanExecute("Hello, World!"));
        }

        [TestMethod]
        public void RelayCommand1_Execute_ValidParameterActionShouldRun()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            RelayCommand<bool> command = new RelayCommand<bool>(
                b =>
                    {
                        autoResetEvent.Set();
                    });

            command.Execute(true);

            Assert.IsTrue(autoResetEvent.WaitOne());
        }

        [TestMethod]
        public void RelayCommand1_Execute_InvalidParameterActionShouldNotRun()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            RelayCommand<bool> command = new RelayCommand<bool>(
                b =>
                    {
                        autoResetEvent.Set();
                    });

            command.Execute("Hello, World!");

            Assert.IsFalse(autoResetEvent.WaitOne(1000));
        }
    }
}
