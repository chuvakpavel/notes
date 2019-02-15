using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Notes.Database;
using Notes.Interfaces;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation NavigationService => Application.Current?.MainPage?.Navigation;

        public event PropertyChangedEventHandler PropertyChanged;

        public IDataService DataService = NotesRepository.Instance;

        public static string ProtectPassword(string password)
        {
            var byteArray = Encoding.UTF8.GetBytes(password);
            var stream = new MemoryStream(byteArray);
            var hashBytes = SHA512.Create().ComputeHash(stream);
            var sBuilder = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sBuilder.Append(b.ToString("x2"));
            }
            var hash = sBuilder.ToString();
            return hash;
        }
    }
}
