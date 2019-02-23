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
    public partial class AddLinkPopUp 
    {
        public event EventHandler<string> AddedLink;
        public AddLinkPopUp()
        {
            InitializeComponent();
        }

        private async void TextCompleted(object sender, EventArgs e)
        {
            AddedLink?.Invoke(this, TextEntry.Text);
            await PopUpsHelper.HidePopUps();
        }
    }
}