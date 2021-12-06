namespace Timetable.Timetable.Model
{
    public class TeacherLoad
    {
        public enum ClassType
        {
            LaboratoryWork,
            Lecture,
            PracticalLesson
        }
        
        public long Id { get; set; }
        public ClassType Type { get; set; }
        public int TotalHours { get; set; }
        
        public long GroupId { get; set; }
        public Group Group { get; set; }
        
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}