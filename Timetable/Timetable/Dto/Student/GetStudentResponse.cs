using Model.Timetable;
using Timetable.Auth.Model;

namespace Timetable.Timetable.Dto.Student
{
    public class GetStudentResponse
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public FullName FullName { get; set; }
        public Address Address { get; set; }
        public long GroupId { get; set; }
        public ParentContacts MotherContacts { get; set; }
        public ParentContacts FatherContacts { get; set; }

        public static GetStudentResponse FromStudent(global::Model.Timetable.Student student)
            => new GetStudentResponse()
            {
                Id = student.Id,
                Email = student.User.Email,
                Address = student.User.Address,
                FullName = student.User.FullName,
                PhoneNumber = student.User.PhoneNumber,
                GroupId = student.GroupId,
                MotherContacts = student.MotherContacts,
                FatherContacts = student.FatherContacts
            };
    }
}