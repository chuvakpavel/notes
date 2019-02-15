using System;
using System.Threading.Tasks;
using Notes.Helpers;
using Notes.Models;
using Notes.Pages.PopUpsPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteListPage : ContentPage
    {
        public NoteListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.Init();
        }

        private async void AddNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditNotePage());
        }

        private async void NoteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var note = ViewModel.SelectedItem;
            if (await CheckPrivateNote(note))
            {
                await Navigation.PushAsync(new AddEditNotePage(note));
            }
            await Task.Delay(500);
            if (sender is ListView lv) lv.SelectedItem = null;

        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            if (sender is MenuItem mi && mi.CommandParameter is Note note)
            {
                if (await CheckPrivateNote(note))
                {
                    await ViewModel.DeleteNote(note);
                }

            }
        }

        private async void ProfileClicked(object sender, EventArgs e)
        {
            await PopUpsHelper.ShowProfilePopUp();
        }

        private async Task<bool> CheckPrivateNote(Note note)
        {
            if (note.IsPrivate)
            {
                return await PopUpsHelper.ShowCheckPasswordPopUp();
            }

            return true;
        }

        private  async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var note = ViewModel.SelectedItem;
            if (sender is ListView lv) lv.SelectedItem = null;
            if (await CheckPrivateNote(note))
            {
                await Navigation.PushAsync(new AddEditNotePage(note));
            }
        }
    }
}