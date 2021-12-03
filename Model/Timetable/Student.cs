using Timetable.Auth.Model;
using Timetable.Timetable.Model;

namespace Model.Timetable
{
    public class Student
    {
        public long Id { get; set; }
        public ParentContacts MotherContacts { get; set; }
        public ParentContacts FatherContacts { get; set; }
        
        public long GroupId { get; set; }
        public Group Group { get; set; }
        
        public TimetableUser User { get; set; }
    }
    
    public class ParentContacts
    {
        public FullName FullName { get; set; }
        public string PhoneNumber { get; set; }

        public ParentContacts(FullName fullName, string phoneNumber)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
        
        public ParentContacts() {}
    }
}