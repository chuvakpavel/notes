using Notes.Helpers;
using Notes.Models;
using Notes.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditNotePage : ContentPage
	{
		public AddEditNotePage(Note note=null)
		{
			InitializeComponent ();
		    BindingContext = new AddEditNoteViewModel(note);
		}

	    private async void Switch_OnToggled(object sender, ToggledEventArgs e)
	    {
	        if (!Preferences.Get(nameof(ConstantHelper.IsPasswordSetName), false))
	        {
	            await PopUpsHelper.ShowProfilePopUp(true);
	        }
	    }
	}
}