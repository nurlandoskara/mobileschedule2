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
    public class GroupsViewModel : BaseViewModel<Group>
    {
        private string _searchText;
        private List<Group> _allItems;
        private ObservableCollection<Group> _items;

        public ObservableCollection<Group> Items
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

        public GroupsViewModel()
        {
            Title = "Сыныптар";
            Items = new ObservableCollection<Group>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private void FilterItems()
        {
            var text = SearchText;
            Items = new ObservableCollection<Group>(_allItems.Where(x => x.DisplayName.ToLower().Contains(text.ToLower())).ToList());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync("api/Groups", true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                _allItems = Items.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Info = "Серверге қосылу мүмкін емес...";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}