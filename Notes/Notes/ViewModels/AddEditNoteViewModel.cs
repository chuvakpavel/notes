using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Notes.Helpers;
using Notes.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
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

        public List<string> TypesNames => Enum.GetNames(typeof(FilesTypes)).ToList();
        public string SelectedType { get; set; }

        public ICommand AddItemCommand { get; set; }

        public AddEditNoteViewModel(Note note)
        {
            SaveNoteCommand = new Command(SaveNote, CanExecuteSaveCommand);
            NoteFiles = new ObservableCollection<NoteFile>();
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
        public async Task DeleteNoteItem(NoteFile noteFile)
        {
            await DataService.DeleteNoteFileAsync(noteFile);
            NoteFiles.Remove(noteFile);
        }
        private bool CanExecuteSaveCommand()
        {
            return !string.IsNullOrEmpty(Title);
        }


        public async void AddItem()
        {
            Enum.TryParse(SelectedType, out FilesTypes result);
            SelectedType = String.Empty;

            switch (result)
            {
                case FilesTypes.Text: await AddText(); break;
                case FilesTypes.Document: await AddDocument(); break;
                case FilesTypes.Link: await AddLink(); break;
            }
        }

        private Task AddLink()
        {
            throw new NotImplementedException();
        }

        private async Task AddText()
        {
            var result = await PopUpsHelper.ShowAddTextPopUp();
            if (!string.IsNullOrEmpty(result))
            {
                var noteFile = new NoteFile() { FileName = result, FileType = FilesTypes.Text };
                NoteFiles.Add(noteFile);
            }
        }

        private async Task AddDocument()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                var noteFile = new NoteFile() { FileName = fileData.FileName, FilePath = fileData.FilePath, FileType = FilesTypes.Document };
                NoteFiles.Add(noteFile);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

    }
}