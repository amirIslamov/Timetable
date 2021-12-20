namespace Model.Entities;

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

    public long DisciplineId { get; set; }
    public Discipline Discipline { get; set; }

    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    
    public List<TimetableEntry> TimetableEntries { get; set; }
}