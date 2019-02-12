using System.Collections.ObjectModel;
using System.Windows.Input;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class AddNoteViewModel : BaseViewModel
    {
        public bool IsPrivate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ObservableCollection<NoteFile> NoteFiles;

        public ICommand SaveNoteCommand { get; set; }

        public AddNoteViewModel()
        {
            SaveNoteCommand = new Command(SaveNote, CanExecuteSaveCommand);
        }

        private async void SaveNote()
        {
            var note = new Note() { Title = Title, Description = Description, IsPrivate = IsPrivate };
            var result = await DataService.SaveNoteAsync(note);
            if (result)
            {
                await NavigationService.PopAsync();
            }
        }

        private bool CanExecuteSaveCommand()
        {
            return string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Description);
        }
    }
}