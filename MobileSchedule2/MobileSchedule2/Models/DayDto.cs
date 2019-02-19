using System.Collections.Generic;

namespace MobileSchedule2.Models
{
    public class DayDto
    {
        public Enums.Enums.WeekDay WeekDay { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
