namespace API.Timetable.Dto.Subject;

public class GetSubjectResponse
{
    public static GetSubjectResponse FromSubject(Model.Entities.Subject subject)
    {
        return new GetSubjectResponse()
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