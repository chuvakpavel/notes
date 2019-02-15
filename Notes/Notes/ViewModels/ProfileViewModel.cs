using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Notes.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public string OldPassword { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public ICommand SavePassCommand { get; set; }

        public bool IsPasswordSet => Preferences.Get(nameof(ConstantHelper.IsPasswordSetName), false);

        public ProfileViewModel()
        {
            SavePassCommand = new Command(ExecuteSaveCommand);
        }

        private async void ExecuteSaveCommand()
        {
            ErrorMessage=String.Empty;
            if (IsPasswordSet)
            {
                if (Preferences.Get(nameof(ConstantHelper.Password), string.Empty) == ProtectPassword(OldPassword))
                {
                    Preferences.Set(nameof(ConstantHelper.Password), ProtectPassword(Password));
                    await PopUpsHelper.HidePopUps();
                }
                else
                {
                    ErrorMessage = "Wrong Old password";
                }
            }
            else
            {
                Preferences.Set(nameof(ConstantHelper.Password), ProtectPassword(Password));
                Preferences.Set(nameof(ConstantHelper.IsPasswordSetName), true);
                await PopUpsHelper.HidePopUps();
            }
        }
        public static string ProtectPassword(string password)
        {
            var byteArray = Encoding.UTF8.GetBytes(password);
            var stream = new MemoryStream(byteArray);
            var hashBytes = SHA512.Create().ComputeHash(stream);
            var sBuilder = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sBuilder.Append(b.ToString("x2"));
            }
            var hash = sBuilder.ToString();
            return hash;
        }
    }
}
