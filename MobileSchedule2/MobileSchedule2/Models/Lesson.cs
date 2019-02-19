using MobileSchedule2.Enums;

namespace MobileSchedule2.Models
{
    public class Lesson: BaseDbObject
    {
        public int Order { get; set; }
        public string SubjectName { get; set; }
        public string GroupOrTeacherName { get; set; }
        public string AuditoryName { get; set; }
        public Enums.Enums.WeekDay WeekDay { get; set; }
        public string Day => EnumHelper.Description(WeekDay);
    }
}
