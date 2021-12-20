namespace API.Timetable.Dto.Discipline;

public class ListDisciplinesResponse
{
    public static ListDisciplinesResponse FromDiscipline(Model.Entities.Discipline discipline)
    {
        return new ListDisciplinesResponse()
        {
            Id = discipline.Id,
            SubjectId = discipline.SubjectId,
            GroupId = discipline.GroupId,
            ControlType = discipline.ControlType,
            SemesterNumber = discipline.SemesterNumber,
            ClassroomWorkHours = discipline.ClassroomWorkHours,
            IndependentWorkHours = discipline.IndependentWorkHours
        };
    }

    public int IndependentWorkHours { get; set; }

    public int ClassroomWorkHours { get; set; }

    public int SemesterNumber { get; set; }

    public Model.Entities.Discipline.SemesterControlType ControlType { get; set; }

    public long GroupId { get; set; }

    public long SubjectId { get; set; }

    public long Id { get; set; }
}