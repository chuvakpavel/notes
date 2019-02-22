using System;
using Notes.Helpers;
using Plugin.Fingerprint;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Notes.Pages.PopUpsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePopUp
    {
        public ProfilePopUp(bool hideCancel)
        {
            InitializeComponent();
            if (hideCancel)
            {
                CancelButton.IsVisible = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Preferences.ContainsKey(nameof(ConstantHelper.IsFingerPrintAvailableName)))
            {
                Preferences.Set(nameof(ConstantHelper.IsFingerPrintAvailableName), await CrossFingerprint.Current.IsAvailableAsync(true));
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            await PopUpsHelper.HidePopUps();
        }
    }
}