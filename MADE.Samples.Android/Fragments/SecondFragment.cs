namespace MADE.Samples.Android.Fragments
{
    using global::Android.Views;

    using MADE.App.Views.Navigation.Pages;
    using MADE.Samples.ViewModels;

    public class SecondFragment : MvvmPage
    {
        public SecondFragment()
        {
            this.DataContext = new SecondPageViewModel();
        }

        public SecondPageViewModel ViewModel => this.DataContext as SecondPageViewModel;

        public override bool HasMenu => true;

        public override int LayoutId => Resource.Layout.SecondFragment;

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            inflater?.Inflate(Resource.Menu.SecondFragmentMenu, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.showdialog)
            {
                this.ViewModel.ShowDialogCommand?.Execute(null);
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}