
using System.Threading.Tasks;
using Notes.Helpers;
using Xamarin.Essentials;

namespace Notes.ViewModels
{
    public class CheckPasswordViewModel : BaseViewModel
    {
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<bool> CheckPassword()
        {
            if (Preferences.Get(nameof(ConstantHelper.Password), string.Empty) == ProtectPassword(Password))
            {
                Preferences.Set(nameof(ConstantHelper.Password), ProtectPassword(Password));
                await PopUpsHelper.HidePopUps();
                return true;
            }

            ErrorMessage = "Wrong Old password";
            return false;
        }
    }
}
