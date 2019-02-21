using System.Threading.Tasks;

namespace Notes.Interfaces
{
    public interface IPlatformDocumentOpener
    {
        /// <summary>
        /// Attempts to resolve whether any app on the system knows how to edit a file with given media type and file
        /// extension.
        /// </summary>
        bool CanOpen(string path);

        ///// <summary>
        ///// Attempts to resolve media type from given file extension, not expected to include a leading dot (.).
        ///// </summary>
        //MediaType GetMediaTypeFromExtension(string extension);

        /// <summary>
        /// Opens file at given path, being of identified media type, and supposed to be having provided file extension.
        /// </summary>
        /// <remarks>
        /// Returns when the user returns to this app, returning true if file could be opened, false otherwise. As there
        /// is now way of telling if any app opening the provided file actually edited it, that has to be resolved via
        /// hashing, or some other means.
        /// </remarks>
        Task<PlatformDocumentOpening> Open(string path);
    }

    public class PlatformDocumentOpening
    {
        /// <summary>
        /// Whether or not the opened file was altered by the program that opened it.
        /// </summary>
        public bool DidChange;

        /// <summary>
        /// Whether or not the file could be opened by another device application.
        /// </summary>
        public bool DidOpen;
    }
}