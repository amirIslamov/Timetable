namespace Model.Entities;

public class TimetableEntry
{
    public long Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public WeekType WeekType { get; set; }
    public int ClassNum { get; set; }
    
    public string Link { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long UpdatedBy { get; set; }

    public long TeacherLoadId { get; set; }
    public TeacherLoad TeacherLoad { get; set; }
    
    public long ClassroomId { get; set; } 
    public Classroom Classroom { get; set; }

    public long GroupId { get; set; }
}

public enum WeekType
{
    Odd,
    Even,
    All
}