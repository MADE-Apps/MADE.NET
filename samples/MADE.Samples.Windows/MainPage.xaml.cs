namespace MADE.Samples.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Data.Validation;
    using global::Windows.UI.Xaml.Controls;
    using MADE.Data.Validation.Validators;
    using UI.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DropDownList.ItemsSource = new List<string>
            {
                "Hello",
                "World",
                "New",
                "DropDown",
                "List",
                "Control"
            };

            this.TextBoxValidator.Validators = new ValidatorCollection { new RequiredValidator(), new EmailValidator() };

            this.DatePickerValidator.Validators = new ValidatorCollection
            {
                new RequiredValidator(), new BetweenValidator(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(7)),
            };
        }

        public ObservableCollection<FilePickerItem> FilePickerFiles { get; } = new ObservableCollection<FilePickerItem>();
    }
}