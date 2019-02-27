using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MADE.Samples.Forms
{
    using System.Diagnostics;

    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            this.TextEntry.TextChanged += TextEntry_TextChanged;
		}

        private void TextEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine(e.NewTextValue);
        }
    }
}
