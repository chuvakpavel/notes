using SQLite;

namespace Notes.Models
{
    [Table("NoteFiles")]
    public class NoteFile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FilesTypes FileType { get; set; }
    }
}
