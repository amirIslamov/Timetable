using System;

namespace Timetable.Timetable.Model
{
    public class TimetableEntry
    {
        public long Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public WeekType WeekType { get; set; }
        public int ClassNum { get; set; }
        
        public long ClassGroupId { get; set; }
        public TeacherLoad TeacherLoad { get; set; }
    }
    
    public enum WeekType
    {
        Odd,
        Even
    }
}