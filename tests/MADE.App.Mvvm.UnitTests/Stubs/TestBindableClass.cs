namespace MADE.App.Mvvm.UnitTests.Stubs
{
    public class TestBindableClass : Bindable
    {
        private bool bindableBool;

        public bool BindableBool
        {
            get => this.bindableBool;
            set => this.Set(() => this.BindableBool, ref this.bindableBool, value);
        }
    }
}