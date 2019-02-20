namespace MobileSchedule2.Models
{
    public enum MenuItemType
    {
        Schedule,
        ScheduleForTeacher,
        Groups,
        Teachers,
        News,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
