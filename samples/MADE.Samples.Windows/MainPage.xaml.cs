namespace MADE.Samples.Windows
{
    using System.Collections.Generic;
    using global::Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DropDownList.ItemsSource = new List<string> { "Hello", "World", "New", "DropDown", "List", "Control" };
        }
    }
}
