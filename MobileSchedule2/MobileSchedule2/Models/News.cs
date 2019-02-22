using SQLite;
using Xamarin.Forms;

namespace MobileSchedule2.Models
{
    public class News: BaseDbObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Ignore] public ImageSource ImageFile { get; set; }
    }
}