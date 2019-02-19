using SQLite;

namespace MobileSchedule2.Models
{
    public class BaseDbObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ServerId { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsForTeacher { get; set; }
        public int GroupOrTeacherId { get; set; }
    }
}
