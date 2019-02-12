using System.Collections.Generic;
using SQLite;

namespace Notes.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsPrivate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        [Ignore]
        public List<NoteFile> NoteFiles { get; set; }
    }
}
