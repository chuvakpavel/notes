using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models;
using SQLite;

namespace Notes.Database
{
    public class NotesRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public NotesRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
            _database.CreateTableAsync<NoteFile>().Wait();
        }

        public async Task<bool> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
            {
                await _database.UpdateAsync(note);
                return note.Id!=0;
            }

            return await _database.InsertAsync(note) != 0;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _database.Table<Note>().ToListAsync();
        }
    }
}
