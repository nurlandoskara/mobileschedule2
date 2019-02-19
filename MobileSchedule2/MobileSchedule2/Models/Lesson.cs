using System;
using System.Collections.Generic;
using System.Text;

namespace MobileSchedule2.Models
{
    public class Lesson: BaseDbObject
    {
        public int Order { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
    }
}
