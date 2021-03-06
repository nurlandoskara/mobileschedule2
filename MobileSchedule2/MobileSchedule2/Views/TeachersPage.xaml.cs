﻿using MobileSchedule2.Models;
using MobileSchedule2.ViewModels;
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

            var editor = App.Preferences.Edit();
            editor.PutInt("TeacherId", item.ServerId);
            editor.Apply();
            App.TeacherId = item.ServerId;
            App.IsTeacherChanged = true;
            await Navigation.PushAsync(new SchedulePage(true));

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