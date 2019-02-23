using System.Threading.Tasks;
using Notes.Pages.PopUpsPages;
using Notes.Pages.PopUpsPages.AddItemPopUps;
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

        public static async Task<string> ShowAddTextPopUp()
        {
            var tcs = new TaskCompletionSource<string>();
            var popUp = new AddTextPopUp();
            popUp.AddedText += (object sender, string result) =>
            {
                tcs.TrySetResult(result);
            };
            await PopupNavigation.Instance.PushAsync(popUp);
            return await tcs.Task;
        }

        public static async Task<string> ShowAddLinkPopUp()
        {
            var tcs = new TaskCompletionSource<string>();
            var popUp = new AddLinkPopUp();
            popUp.AddedLink += (object sender, string result) =>
            {
                tcs.TrySetResult(result);
            };
            await PopupNavigation.Instance.PushAsync(popUp);
            return await tcs.Task;
        }
    }
}
