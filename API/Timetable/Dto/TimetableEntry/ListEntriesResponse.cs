using Model.Entities;

namespace API.Timetable.Dto.TimetableEntry;

public class ListEntriesResponse
{
    public static ListEntriesResponse FromEntry(Model.Entities.TimetableEntry entry)
    {
        return new ListEntriesResponse()
        {
            Id = entry.Id,
            Link = entry.Link,
            Classroom = entry.Classroom,
            ClassNum = entry.ClassNum,
            TeacherLoadId = entry.TeacherLoadId,
            WeekType = entry.WeekType,
            DayOfWeek = entry.DayOfWeek,
        };
    }

    public DayOfWeek DayOfWeek { get; set; }

    public WeekType WeekType { get; set; }

    public long TeacherLoadId { get; set; }

    public int ClassNum { get; set; }

    public string Classroom { get; set; }

    public string Link { get; set; }

    public long Id { get; set; }
}