namespace API.Timetable.Dto.TimetableException;

public class CreateExceptionRequest
{
    public string Classroom { get; set; }
    public DateTime Date { get; set; }
    public string Link { get; set; }
    public int ClassNum { get; set; }
    public long TimetableEntryId { get; set; }
    public long TeacherId { get; set; }
}