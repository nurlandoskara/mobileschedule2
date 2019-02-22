using MobileSchedule2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileSchedule2.ViewModels
{
    public class NewsViewModel : BaseViewModel<News>
    {
        private string _searchText;
        private List<News> _allItems;
        private ObservableCollection<News> _items;

        public ObservableCollection<News> Items
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

        public NewsViewModel()
        {
            Title = "Хабарландырулар";
            Items = new ObservableCollection<News>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private void FilterItems()
        {
            var text = SearchText;
            Items = new ObservableCollection<News>(_allItems.Where(x => x.Title.ToLower().Contains(text.ToLower())).ToList());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync("api/News", true);
                foreach (var item in items)
                {
                    item.ImageFile = Base64ToBitmap(item.Image);
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


        private ImageSource Base64ToBitmap(string base64String)
        {
            return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64String)));
        }
    }
}