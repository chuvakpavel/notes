using System;
using Notes.Helpers;
using Xamarin.Forms.Xaml;

namespace Notes.Pages.PopUpsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePopUp 
	{
		public ProfilePopUp (bool hideCancel)
		{
			InitializeComponent ();
		    if (hideCancel)
		    {
		        CancelButton.IsVisible = false;
		    }
		}
	    protected override bool OnBackButtonPressed()
	    {
	        return true;
	    }

	    private async void CancelClicked(object sender, EventArgs e)
	    {
	       await PopUpsHelper.HidePopUps();
	    }
	}
}