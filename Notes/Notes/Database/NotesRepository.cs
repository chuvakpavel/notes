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
            int result;
            if (note.Id != 0)
            {
                result = await _database.UpdateAsync(note);

            }
            else
            {
                result = await _database.InsertAsync(note);
            }


            var res = result != 0;
            if (res)
            {
                await SaveNoteFiles(note);
            }

            return res;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            var notes = await _database.Table<Note>().ToListAsync();
            if (notes!=null && notes.Count>0)
            {
                foreach (var note in notes)
                {
                    note.NoteFiles = await GetNoteFiles(note);
                }
            }
            return notes;
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            return await _database.DeleteAsync(note);
        }

        private async Task SaveNoteFiles(Note note)
        {
            if (note.NoteFiles.Count > 0)
            {
                foreach (var noteFile in note.NoteFiles)
                {
                    noteFile.NoteId = note.Id;
                    if (noteFile.Id == 0)
                    {
                        await _database.InsertAsync(noteFile);
                    }
                    else
                    {
                        await _database.UpdateAsync(noteFile);
                    }
                }
            }
        }

        private async Task<List<NoteFile>> GetNoteFiles(Note note)
        {
            return await _database.Table<NoteFile>().Where(t => t.NoteId == note.Id).ToListAsync();
        }

    }
}
