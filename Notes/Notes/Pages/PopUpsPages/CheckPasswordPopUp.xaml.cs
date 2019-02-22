using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Helpers;
using Plugin.Fingerprint;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages.PopUpsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckPasswordPopUp
    {
        public event EventHandler<bool> PasswordChecked;
        public CheckPasswordPopUp()
        {
            InitializeComponent();

            FingerPrintButton.IsVisible = Preferences.Get(nameof(ConstantHelper.IsFingerPrintAvailableName), false);
        }

        private async void ApplyClicked(object sender, EventArgs e)
        {
            var result = await ViewModel.CheckPassword();
            if (result)
            {
                PasswordChecked?.Invoke(this, true);
            }
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            PasswordChecked?.Invoke(this, false);
            await PopUpsHelper.HidePopUps();
        }

        private async void FingerprintClicked(object sender, EventArgs e)
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync("Use fingerprint for get access to private data");
            PasswordChecked?.Invoke(this, result.Authenticated);
            await PopUpsHelper.HidePopUps();
        }
    }
}