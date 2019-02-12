using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Notes.Interfaces;
using Notes.Models;
using SQLite;

namespace Notes.Database
{
    public class NotesRepository : IDataService
    {
        private static NotesRepository _instance;
        public static NotesRepository Instance => _instance ?? (_instance = new NotesRepository());

        private readonly SQLiteAsyncConnection _database;

        public NotesRepository()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotesSQLite.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
            _database.CreateTableAsync<NoteFile>().Wait();
        }

        public async Task<bool> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
            {
                await _database.UpdateAsync(note);
                return note.Id != 0;
            }

            return await _database.InsertAsync(note) != 0;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _database.Table<Note>().ToListAsync();
        }
    }
}
