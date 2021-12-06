using Timetable.Timetable.Model;

namespace Model.Timetable
{
    public class Discipline
    {
        public enum SemesterControlType
        {
            Offset,
            Exam
        }
        
        public long Id { get; set; }
        
        public int SemesterNumber { get; set; }
        public int LecturesHours { get; set; }
        public int PracticalLessonsHours { get; set; }
        public int IndependentWorkHours { get; set; }
        public SemesterControlType ControlType { get; set; }
        
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}