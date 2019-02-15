using System.Threading.Tasks;
using Notes.Pages.PopUpsPages;
using Rg.Plugins.Popup.Services;

namespace Notes.Helpers
{
    public class PopUpsHelper
    {
        public static async Task ShowProfilePopUp(bool hideCancel=false)
        {
            await PopupNavigation.Instance.PushAsync(new ProfilePopUp(hideCancel));
        }

        public static async Task HidePopUps()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

    }
}
