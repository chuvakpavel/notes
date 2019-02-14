using System;
using Notes.Models;
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
            await Navigation.PushAsync(new AddEditNotePage(ViewModel.SelectedItem));
            ViewModel.SelectedItem = null;
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            if (sender is MenuItem mi && mi.CommandParameter is Note note)
            {
                await ViewModel.DeleteNote(note);
            }
        }
    }
}