using System.ComponentModel;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation NavigationService => Application.Current?.MainPage?.Navigation;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
