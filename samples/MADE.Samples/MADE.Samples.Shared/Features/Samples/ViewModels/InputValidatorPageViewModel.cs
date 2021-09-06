namespace MADE.Samples.Features.Samples.ViewModels
{
    using System;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Data.Validation;
    using MADE.Data.Validation.Validators;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;

    public class InputValidatorPageViewModel : PageViewModel
    {
        private string inputText;
        private DateTimeOffset? inputDate;

        public InputValidatorPageViewModel(INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
        }

        public ValidatorCollection InputTextValidators { get; } = new ValidatorCollection { new RequiredValidator(), new MaxLengthValidator(16) };

        public string InputText
        {
            get => inputText;
            set => this.SetProperty(ref inputText, value);
        }

        public ValidatorCollection InputDateValidators { get; } = new ValidatorCollection { new RequiredValidator(), new BetweenValidator(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now.AddDays(7)) };

        public DateTimeOffset? InputDate
        {
            get => inputDate;
            set => this.SetProperty(ref inputDate, value);
        }
    }
}