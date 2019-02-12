using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPasswordPage : ContentPage
	{
		public NewPasswordPage ()
		{
			InitializeComponent ();
		}

	    private void AddNoteButtonClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new AddNotePage());
	    }
	}
}