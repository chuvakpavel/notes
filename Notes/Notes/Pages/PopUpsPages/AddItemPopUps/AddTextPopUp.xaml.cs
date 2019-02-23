using Notes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages.PopUpsPages.AddItemPopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTextPopUp 
    {
        public event EventHandler<string> AddedText;
        public AddTextPopUp()
        {
            InitializeComponent();
        }

        private async void TextCompleted(object sender, EventArgs e)
        {
            AddedText?.Invoke(this, TextEntry.Text);
            await PopUpsHelper.HidePopUps();
        }
    }
}