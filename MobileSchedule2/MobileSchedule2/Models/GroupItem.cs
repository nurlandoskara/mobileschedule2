using System.Collections.ObjectModel;

namespace MobileSchedule2.Models
{
    public class GroupItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ObservableCollection<Lesson> Items { get; set; }
    }
}
