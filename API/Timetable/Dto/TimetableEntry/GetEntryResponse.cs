using Model.Entities;

namespace API.Timetable.Dto.TimetableEntry;

public class GetEntryResponse
{
    public static GetEntryResponse FromEntry(Model.Entities.TimetableEntry entry)
    {
        return new GetEntryResponse()
        {
            Id = entry.Id,
            Link = entry.Link,
            ClassroomId = entry.ClassroomId,
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

    public long ClassroomId { get; set; }

    public string Link { get; set; }

    public long Id { get; set; }
}