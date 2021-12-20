namespace API.Timetable.Dto.Subject;

public class ListSubjectsResponse
{
    public static ListSubjectsResponse FromSubject(Model.Entities.Subject subject)
    {
        return new ListSubjectsResponse()
        {
            Id = subject.Id,
            Code = subject.Code,
            Name = subject.Name
        };
    }

    public string Name { get; set; }

    public string Code { get; set; }

    public long Id { get; set; }
}