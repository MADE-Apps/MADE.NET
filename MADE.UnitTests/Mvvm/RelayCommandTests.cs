namespace MADE.UnitTests.Mvvm
{
    using System.Threading;
    using System.Windows.Input;

    using MADE.App.Mvvm;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        public void RelayCommand_CanExecute_ShouldPass()
        {
            RelayCommand command = new RelayCommand(() => { });
            Assert.IsTrue(command.CanExecute());
        }

        [TestMethod]
        public void RelayCommand_Execute_ActionShouldRun()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            RelayCommand command = new RelayCommand(
                () =>
                    {
                        autoResetEvent.Set();
                    });

            command.Execute();

            Assert.IsTrue(autoResetEvent.WaitOne());
        }

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