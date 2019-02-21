using System;
using System.Threading;
using Notes.Helpers;
using Notes.Interfaces;
using Notes.Models;
using Notes.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditNotePage
    {
        public IPlatformDocumentOpener DocumentOpener = DependencyService.Get<IPlatformDocumentOpener>();
        private AddEditNoteViewModel _viewModel;
        public AddEditNotePage(Note note = null)
        {
            InitializeComponent();
            _viewModel=new AddEditNoteViewModel(note);
            BindingContext = _viewModel;
        }

        private async void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (!Preferences.Get(nameof(ConstantHelper.IsPasswordSetName), false))
            {
                await PopUpsHelper.ShowProfilePopUp(true);
            }
        }

        private async void OpenClicked(object sender, EventArgs e)
        {
            if (sender is MenuItem mi && mi.CommandParameter is NoteFile noteFile)
            {
                if (DocumentOpener.CanOpen(noteFile.FilePath))
                {
                    var res = await DocumentOpener.Open(noteFile.FilePath);
                }
                else
                {
                   await DisplayAlert("Error", "Can't open","Ok");
                }

            }
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            if (sender is MenuItem mi && mi.CommandParameter is NoteFile noteFile)
            {
                await _viewModel.DeleteNoteItem(noteFile);
            }
        }
    }
}
