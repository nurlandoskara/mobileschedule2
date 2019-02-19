namespace MobileSchedule2.Models
{
    public class Teacher: BaseDbObject
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PatronymicName { get; set; }
        public string DisplayName { get; set; }
    }
}
