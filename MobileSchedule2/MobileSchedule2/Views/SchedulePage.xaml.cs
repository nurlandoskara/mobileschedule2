
using MobileSchedule2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSchedule2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        private readonly ScheduleViewModel _viewModel;

        public SchedulePage(bool isForTeacher= false)
        {
            InitializeComponent();
            BindingContext = _viewModel = new ScheduleViewModel(isForTeacher);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}