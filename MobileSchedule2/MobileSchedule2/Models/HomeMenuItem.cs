using System;
using System.Collections.Generic;
using System.Text;

namespace MobileSchedule2.Models
{
    public enum MenuItemType
    {
        Schedule,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
