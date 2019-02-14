using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class AddEditNoteViewModel : BaseViewModel
    {
        public string PageTitle { get; set; }
        public bool IsPrivate { get; set; }
        public int Id { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                ((Command)SaveNoteCommand).ChangeCanExecute();
            }
        }

        public string Description { get; set; }

        public ObservableCollection<NoteFile> NoteFiles { get; set; }

        public ICommand SaveNoteCommand { get; set; }

        public AddEditNoteViewModel(Note note)
        {
            SaveNoteCommand = new Command(SaveNote, CanExecuteSaveCommand);
            if (note == null)
            {
                PageTitle = "Add New Note";
            }
            else
            {
                PageTitle = "Edit Note";
                Id = note.Id;
                Title = note.Title;
                Description = note.Description;
                IsPrivate = note.IsPrivate;
                NoteFiles = new ObservableCollection<NoteFile>();
                NoteFiles = note.NoteFiles != null ? new ObservableCollection<NoteFile>(note.NoteFiles) : new ObservableCollection<NoteFile>();

            }
        }

        private async void SaveNote()
        {
            var note = new Note { Id = Id, Title = Title, Description = Description, IsPrivate = IsPrivate, NoteFiles = NoteFiles?.ToList() };
            var result = await DataService.SaveNoteAsync(note);
            if (result)
            {
                await NavigationService.PopAsync();
            }
        }

        private bool CanExecuteSaveCommand()
        {
            return !string.IsNullOrEmpty(Title);
        }
    }
}