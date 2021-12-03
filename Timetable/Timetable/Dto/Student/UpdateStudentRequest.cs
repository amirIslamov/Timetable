using Model.Timetable;

namespace Timetable.Timetable.Dto.Student
{
    public class UpdateStudentRequest
    {
        public ParentContacts MotherContacts { get; set; }
        public ParentContacts FatherContacts { get; set; }
        
        public long GroupId { get; set; }
    }
}