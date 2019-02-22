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
    public partial class NewsPage : ContentPage
    {
        private readonly NewsViewModel _viewModel;
        private static MainPage RootPage => Application.Current.MainPage as MainPage;

        public NewsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}