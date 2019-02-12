using System;
using System.IO;
using Notes.Database;
using Notes.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Notes
{
    public partial class App : Application
    {
        static NotesRepository _database;

        public App()
        {
            // Initialize Live Reload.
#if DEBUG
            LiveReload.Init();
#endif

            InitializeComponent();

            MainPage = new NavigationPage(new NewPasswordPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static NotesRepository Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new NotesRepository(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "NotesSQLite.db3"));
                }

                return _database;
            }
        }
    }
}
