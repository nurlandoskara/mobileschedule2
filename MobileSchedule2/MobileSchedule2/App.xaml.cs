using Android.Content;
using Android.Preferences;
using MobileSchedule2.Models;
using MobileSchedule2.Views;
using SQLite;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileSchedule2
{
    public partial class App : Application
    {
        public static int GroupId { get; set; }
        public static int TeacherId { get; set; }
        public static string BackendUrl = "http://schoolschedule.azurewebsites.net/";
        public static bool IsGroupChanged;
        public static bool IsTeacherChanged;

        public static bool IsOnline => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public static ISharedPreferences Preferences => PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);

        public App()
        {
            InitializeComponent();
            Plugin.Iconize.Iconize
                .With(new Plugin.Iconize.Fonts.IoniconsModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            MainPage = new MainPage();
        }

        private static SQLiteAsyncConnection _dbConnection;

        public static SQLiteAsyncConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schoolschedule.db3");

                    _dbConnection = new SQLiteAsyncConnection(dbPath);
                    try
                    {
                        _dbConnection.CreateTableAsync<Group>().Wait();
                        _dbConnection.CreateTableAsync<Lesson>().Wait();
                        _dbConnection.CreateTableAsync<Teacher>().Wait();
                        _dbConnection.CreateTableAsync<News>().Wait();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                return _dbConnection;
            }
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
    }
}
