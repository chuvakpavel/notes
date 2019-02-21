using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models;

namespace Notes.Interfaces
{
    public interface IDataService
    {
        Task<bool> SaveNoteAsync(Note note);

        Task<List<Note>> GetAllNotesAsync();
        Task<int> DeleteNoteAsync(Note note);
        Task<int> DeleteNoteFileAsync(NoteFile noteFile);
    }
}
