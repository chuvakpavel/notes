using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NoteListViewModel : BaseViewModel
    {
        public ObservableCollection<Note> NoteList { get; set; }
        public Note SelectedItem { get; set; }

        public async Task Init()
        {
            var notes = await DataService.GetAllNotesAsync();
            NoteList = new ObservableCollection<Note>(notes);
        }

        public async Task DeleteNote(Note note)
        {
            var result = await DataService.DeleteNoteAsync(note);
            if (result == 1)
            {
                NoteList.Remove(note);
            }

            //show deleting error
        }
    }
}
