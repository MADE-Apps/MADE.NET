namespace MADE.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using MADE.Data.Validation;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Validators;
    using MADE.UI.Controls;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.TextBoxValidator.Validators =
                new ValidatorCollection { new RequiredValidator(), new EmailValidator() };

            this.DatePickerValidator.Validators = new ValidatorCollection
                                                  {
                                                      new RequiredValidator(),
                                                      new BetweenValidator(
                                                          DateTimeOffset.Now,
                                                          DateTimeOffset.Now.AddDays(7)),
                                                  };

            this.FilePickerControl.ItemClick += (sender, args) => System.Diagnostics.Debug.WriteLine(args.ClickedItem);
        }
        
        public ObservableCollection<FilePickerItem> FilePickerFiles { get; } = new ObservableCollection<FilePickerItem>();
    }
}
