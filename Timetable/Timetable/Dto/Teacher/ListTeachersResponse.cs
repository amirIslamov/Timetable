using System.Collections.Generic;
using System.Linq;
using Repositories.Util;
using Timetable.Auth.Model;

namespace Timetable.Timetable.Dto.Teacher
{
    public class ListTeachersResponse
    {
        public List<ShortTeacher> Students { get; set; }
        public Paging Paging { get; set; }

        public class ShortTeacher
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public FullName FullName { get; set; }
            public string Chair { get; set; }
            public static ShortTeacher FromTeacher(Model.Teacher teacher) => new ListTeachersResponse.ShortTeacher()
            {
                Id = teacher.Id,
                Email = teacher.User.Email,
                FullName = teacher.User.FullName,
                Chair = teacher.Chair
            };
        }

        public static ListTeachersResponse FromTeachersAndPaging(IEnumerable<Model.Teacher> teachers, Paging paging)
            => new ListTeachersResponse()
            {
                Paging = paging,
                Students = teachers.Select(ShortTeacher.FromTeacher).ToList()
            };
    }
}