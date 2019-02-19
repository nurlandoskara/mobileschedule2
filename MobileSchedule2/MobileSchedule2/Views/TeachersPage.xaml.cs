using MobileSchedule2.Models;
using MobileSchedule2.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSchedule2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeachersPage : ContentPage
    {
        private readonly TeachersViewModel _viewModel;
        private static MainPage RootPage => Application.Current.MainPage as MainPage;

        public TeachersPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TeachersViewModel();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Teacher;
            if (item == null)
                return;

            App.TeacherId = item.Id;
            await RootPage.NavigateFromMenu((int) MenuItemType.ScheduleForTeacher);

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