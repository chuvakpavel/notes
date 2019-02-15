using System.Threading.Tasks;
using Notes.Pages.PopUpsPages;
using Rg.Plugins.Popup.Services;

namespace Notes.Helpers
{
    public class PopUpsHelper
    {
        public static async Task ShowProfilePopUp(bool hideCancel = false)
        {
            await PopupNavigation.Instance.PushAsync(new ProfilePopUp(hideCancel));
        }

        public static async Task HidePopUps()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        public static async Task<bool> ShowCheckPasswordPopUp()
        {
            var tcs = new TaskCompletionSource<bool>();
            var popUp = new CheckPasswordPopUp();
            popUp.PasswordChecked += (object sender, bool result) =>
            {
                tcs.TrySetResult(result);
            };
            await PopupNavigation.Instance.PushAsync(popUp);
            return await tcs.Task;
        }

        private static void PopUp_PasswordChecked(object sender, bool e)
        {
            throw new System.NotImplementedException();
        }
    }
}
