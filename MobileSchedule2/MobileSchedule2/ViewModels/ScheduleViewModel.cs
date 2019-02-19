using MobileSchedule2.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileSchedule2.ViewModels
{
    public class ScheduleViewModel : BaseViewModel<Lesson>
    {
        public ObservableCollection<Lesson> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ScheduleViewModel()
        {
            Title = "Сабақ кестесі";
            Items = new ObservableCollection<Lesson>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true, App.GroupId);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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