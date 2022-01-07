namespace Model.Entities;

public class TimetableException
{
    public long Id { get; set; }

    public long TimetableEntryId { get; set; }
    public TimetableEntry TimetableEntry { get; set; }

    public DateTime ActualDate { get; set; }
    public DateTime Date { get; set; }
    public int ClassNum { get; set; }

    public string Link { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long UpdatedBy { get; set; }

    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    
    public long ClassroomId { get; set; }
    public Classroom Classroom { get; set; }
}