using System.Collections.Generic;
using System.Linq;
using Repositories.Util;
using Timetable.Auth.Model;

namespace Timetable.Timetable.Dto.Student
{
    public class ListStudentsResponse
    {
        public List<ShortStudent> Students { get; set; }
        public Paging Paging { get; set; }

        public class ShortStudent
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public FullName FullName { get; set; }
            public long GroupId { get; set; }
            public static ShortStudent FromStudent(global::Model.Timetable.Student student) => new ListStudentsResponse.ShortStudent()
            {
                Id = student.Id,
                Email = student.User.Email,
                FullName = student.User.FullName,
                GroupId = student.GroupId
            };
        }

        public static ListStudentsResponse FromStudentsAndPaging(IEnumerable<global::Model.Timetable.Student> users, Paging paging)
            => new ListStudentsResponse()
            {
                Paging = paging,
                Students = users.Select(ShortStudent.FromStudent).ToList()
            };
    }
}