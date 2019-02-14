using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Models;
using Notes.ViewModels;
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
	}
}