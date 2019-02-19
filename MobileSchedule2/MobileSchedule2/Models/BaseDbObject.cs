using SQLite;

namespace MobileSchedule2.Models
{
    public class BaseDbObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
