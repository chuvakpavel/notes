using System.ComponentModel;
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
    }
}
