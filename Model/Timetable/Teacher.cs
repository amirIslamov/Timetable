using System.Collections.Generic;
using Timetable.Auth.Model;

namespace Timetable.Timetable.Model
{
    public class Teacher
    {
        public long Id { get; set; }
        
        public string Chair { get; set; }
        public TimetableUser User { get; set; }

        public List<ClassGroup> ClassGroups { get; set; }
    }
}