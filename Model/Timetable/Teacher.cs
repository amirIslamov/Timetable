using System.Collections.Generic;
using Model.Profile;

namespace Timetable.Timetable.Model
{
    public class Teacher
    {
        public long Id { get; set; }
        
        public string Chair { get; set; }
        public TimetableUser User { get; set; }

        public List<TeacherLoad> ClassGroups { get; set; }
    }
}