using MobileSchedule2.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileSchedule2.ViewModels
{
    public class ScheduleViewModel : BaseViewModel<Lesson>
    {
        private readonly bool _isForTeacher;
        public ObservableCollection<Lesson> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ScheduleViewModel(bool isForTeacher = false)
        {
            _isForTeacher = isForTeacher;
            Title = isForTeacher ? "Сабақ кестесі" : "Мұғалім кестесі";
            Items = new ObservableCollection<Lesson>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var api = !_isForTeacher ? $"api/Schedule?groupId={App.GroupId}" : $"api/TSchedule?teacherId={App.TeacherId}";
                var id = !_isForTeacher ? App.GroupId : App.TeacherId;
                Items.Clear();
                var items = await DataStore.GetItemsAsync(api, id, _isForTeacher, true);
                foreach (var item in items.OrderBy(x => x.WeekDay).ThenBy(x => x.Order))
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