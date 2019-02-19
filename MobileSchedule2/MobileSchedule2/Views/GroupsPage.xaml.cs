using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileSchedule2.Models;
using MobileSchedule2.Views;
using MobileSchedule2.ViewModels;

namespace MobileSchedule2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        private readonly GroupsViewModel _viewModel;
        private static MainPage RootPage => Application.Current.MainPage as MainPage;

        public GroupsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GroupsViewModel();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Group;
            if (item == null)
                return;

            App.GroupId = item.Id;
            await RootPage.NavigateFromMenu((int) MenuItemType.Schedule);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}