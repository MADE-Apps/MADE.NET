namespace MADE.Samples
{
    using System;

    using MADE.Data.Validation;
    using MADE.Data.Validation.Validators;

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
        }
    }
}
