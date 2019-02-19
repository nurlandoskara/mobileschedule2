using MobileSchedule2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileSchedule2.ViewModels
{
    public class TeachersViewModel : BaseViewModel<Teacher>
    {
        private string _searchText;
        private List<Teacher> _allItems;
        private ObservableCollection<Teacher> _items;

        public ObservableCollection<Teacher> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public Command LoadItemsCommand { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterItems();
            }
        }

        public TeachersViewModel()
        {
            Title = "Мұғалімдер";
            Items = new ObservableCollection<Teacher>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private void FilterItems()
        {
            var text = SearchText;
            Items = new ObservableCollection<Teacher>(_allItems.Where(x => x.DisplayName.ToLower().Contains(text.ToLower())).ToList());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync("api/Teachers", true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                _allItems = Items.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}