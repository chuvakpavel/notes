using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Helpers;
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
    }
}