using System;
using System.Collections.Generic;
using System.Text;

namespace MobileSchedule2.Models
{
    public class Group: BaseDbObject
    {
        public string DisplayName { get; set; }
        public int ClassYear { get; set; }
        public string ClassLetter { get; set; }
    }
}
