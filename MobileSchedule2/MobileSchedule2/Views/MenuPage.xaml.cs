using MobileSchedule2.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSchedule2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private static MainPage RootPage => Application.Current.MainPage as MainPage;

        public MenuPage()
        {
            InitializeComponent();

            var menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Schedule, Title="Сабақ кестесі", Icon = "fas-calendar-alt" },
                new HomeMenuItem {Id = MenuItemType.Groups, Title="Сыныптар", Icon = "fas-list-ol" },
                new HomeMenuItem {Id = MenuItemType.News, Title="Жаңалықтар", Icon = "fas-newspaper" },
                new HomeMenuItem {Id = MenuItemType.Settings, Title="Баптаулар", Icon = "fas-cog" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}