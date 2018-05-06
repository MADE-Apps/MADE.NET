namespace MADE.UnitTests.Mocks
{
    using MADE.App.Mvvm;

    public class MockBindable : Bindable
    {
        private bool bindableBool;

        public bool BindableBool
        {
            get => this.bindableBool;
            set => this.Set(() => this.BindableBool, ref this.bindableBool, value);
        }
    }
}