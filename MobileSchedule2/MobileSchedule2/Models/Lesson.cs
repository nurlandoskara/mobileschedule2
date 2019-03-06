using System;
using MobileSchedule2.Enums;

namespace MobileSchedule2.Models
{
    public class Lesson: BaseDbObject
    {
        public int Order { get; set; }
        public string LessonTime => Classes.LessonTimes()[Order];
        public string SubjectName { get; set; }
        public string GroupOrTeacherName { get; set; }
        public string AuditoryName { get; set; }
        public Enums.Enums.WeekDay WeekDay { get; set; }
        public string Day => EnumHelper.Description(WeekDay);
        public int StartHour => Convert.ToInt32(LessonTime.Substring(0, 2));
        public int StartMinute => Convert.ToInt32(LessonTime.Substring(3, 2));
        public int EndHour => Convert.ToInt32(LessonTime.Substring(6, 2));
        public int EndMinute => Convert.ToInt32(LessonTime.Substring(9, 2));

        public bool IsCurrent => (int) WeekDay == (int) DateTime.Now.DayOfWeek && 
                                 ( DateTime.Now.Hour > StartHour || DateTime.Now.Hour == StartHour && DateTime.Now.Minute >= StartMinute) && 
                                 ( DateTime.Now.Hour < EndHour || DateTime.Now.Hour == EndHour && DateTime.Now.Minute <= EndMinute);
    }
}
