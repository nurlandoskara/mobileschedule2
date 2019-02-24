
using System;
using System.Linq;
using MobileSchedule2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSchedule2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly bool _isForTeacher;

        public SchedulePage(bool isForTeacher= false)
        {
            _isForTeacher = isForTeacher;
            InitializeComponent();
            BindingContext = _viewModel = new ScheduleViewModel(isForTeacher);
        }

        public SchedulePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ScheduleViewModel(false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_isForTeacher)
            {
                if (App.TeacherId == 0) Navigation.PushAsync(new TeachersPage());
            }
            else
            {
                if (App.GroupId == 0) Navigation.PushAsync(new GroupsPage());
            }

            if (_viewModel.GroupItems.Count == 0 || App.IsGroupChanged || App.IsTeacherChanged)
            {
                App.IsGroupChanged = false;
                App.IsTeacherChanged = false;
                _viewModel.LoadItemsCommand.Execute(null);
            }
        }


        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var child = (sender as StackLayout)?.Children?.First(x => x.GetType() == typeof(StackLayout));
            if (child != null)
            {
                child.IsVisible = !child.IsVisible;
            }
        }

        private void ItemsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}