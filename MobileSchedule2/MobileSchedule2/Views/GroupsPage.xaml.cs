
using MobileSchedule2.Models;
using MobileSchedule2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            var editor = App.Preferences.Edit();
            editor.PutInt("GroupId", item.Id);
            editor.Apply();
            App.GroupId = item.Id;
            App.IsGroupChanged = true;
            await Navigation.PushAsync(new SchedulePage());

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