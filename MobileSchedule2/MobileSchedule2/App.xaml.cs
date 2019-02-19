using System;
using System.IO;
using MobileSchedule2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileSchedule2.Views;
using SQLite;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileSchedule2
{
    public partial class App : Application
    {

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

        public static int GroupId { get; set; }

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
