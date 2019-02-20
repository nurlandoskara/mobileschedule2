using MobileSchedule2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSchedule2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private readonly Dictionary<int, NavigationPage> _menuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            _menuPages.Add((int)MenuItemType.Schedule, (NavigationPage)Detail);
            App.GroupId = App.Preferences.GetInt("GroupId", 0);
            App.TeacherId = App.Preferences.GetInt("TeacherId", 0);
            if (App.GroupId == 0) FirstRun();
        }

        private async void FirstRun()
        {
            await NavigateFromMenu((int)MenuItemType.Groups);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!_menuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Schedule:
                        if (App.GroupId == 0) await NavigateFromMenu((int) MenuItemType.Groups);
                        else _menuPages.Add(id, new NavigationPage(new SchedulePage()));
                        break;
                    case (int)MenuItemType.ScheduleForTeacher:
                        if (App.TeacherId == 0) await NavigateFromMenu((int)MenuItemType.Teachers);
                        else _menuPages.Add(id, new NavigationPage(new SchedulePage(true)));
                        break;
                    case (int)MenuItemType.Groups:
                        _menuPages.Add(id, new NavigationPage(new GroupsPage()));
                        break;
                    case (int)MenuItemType.Teachers:
                        _menuPages.Add(id, new NavigationPage(new TeachersPage()));
                        break;
                    case (int)MenuItemType.News:
                        _menuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                    case (int)MenuItemType.Settings:
                        _menuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                }
            }

            var newPage = _menuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}