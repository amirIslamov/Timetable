using Model.Entities;

namespace API.Timetable.Dto.TimetableEntry;

public class CreateEntryRequest
{
    public string Classroom { get; set; }
    public string Link { get; set; }
    public int ClassNum { get; set; }
    public long TeacherLoadId { get; set; }
    public WeekType WeekType { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}