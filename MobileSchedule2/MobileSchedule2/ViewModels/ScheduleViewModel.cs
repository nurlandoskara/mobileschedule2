using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MobileSchedule2.Enums;
using MobileSchedule2.Models;
using Xamarin.Forms;

namespace MobileSchedule2.ViewModels
{
    public class ScheduleViewModel : BaseViewModel<Lesson>
    {
        private readonly bool _isForTeacher;
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<GroupItem> GroupItems { get; set; }

        public ScheduleViewModel(bool isForTeacher = false)
        {
            _isForTeacher = isForTeacher;
            Title = !isForTeacher ? "Сабақ кестесі" : "Мұғалім кестесі";
            GroupItems = new ObservableCollection<GroupItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async void LoadInfo()
        {
            if (_isForTeacher)
            {
                var teacherName = await App.DbConnection.Table<Teacher>()
                    .FirstOrDefaultAsync(x => x.ServerId == App.TeacherId);
                if (teacherName != null) Info = teacherName.DisplayName;
            }
            else
            {
                var groupName = await App.DbConnection.Table<Group>()
                    .FirstOrDefaultAsync(x => x.ServerId == App.GroupId);
                if (groupName != null) Info = groupName.DisplayName;
            }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                LoadInfo();
                var api = !_isForTeacher ? $"api/Schedule?groupId={App.GroupId}" : $"api/TSchedule?teacherId={App.TeacherId}";
                var id = !_isForTeacher ? App.GroupId : App.TeacherId;

                GroupItems.Clear();
                var items = await DataStore.GetLessonsAsync(api, id, _isForTeacher, true);
                foreach (var item in items.GroupBy(x => x.WeekDay).OrderBy(x => x.Key))
                {
                    var groupItem = new GroupItem
                    {
                        IsCurrent = (int)item.Key == (int)DateTime.Now.DayOfWeek,
                        Title = EnumHelper.Description(item.Key),
                        Items = new ObservableCollection<Lesson>(item.OrderBy(x => x.Order).ToList())
                    };
                    GroupItems.Add(groupItem);
                }
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