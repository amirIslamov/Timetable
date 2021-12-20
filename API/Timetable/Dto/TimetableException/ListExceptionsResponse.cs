namespace API.Timetable.Dto.TimetableException;

public class ListExceptionsResponse
{
    public static ListExceptionsResponse FromException(Model.Entities.TimetableException exception)
    {
        return new ListExceptionsResponse()
        {
            Classroom = exception.Classroom,
            Date = exception.Date,
            Id = exception.Id,
            Link = exception.Link,
            CassNum = exception.ClassNum,
            TeacherId = exception.TeacherId,
            UpdatedAt = exception.UpdatedAt,
            TimetableEntryId = exception.TimetableEntryId
        };
    }

    public long TimetableEntryId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long TeacherId { get; set; }

    public int CassNum { get; set; }

    public string Link { get; set; }

    public long Id { get; set; }

    public DateTime Date { get; set; }

    public string Classroom { get; set; }
}