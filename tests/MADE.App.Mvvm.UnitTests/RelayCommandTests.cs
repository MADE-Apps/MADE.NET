namespace MADE.App.Mvvm.UnitTests
{
    using System.Threading;

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
    }
}