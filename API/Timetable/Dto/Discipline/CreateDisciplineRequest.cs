namespace API.Timetable.Dto.Discipline;

public class CreateDisciplineRequest
{
    public Model.Entities.Discipline.SemesterControlType ControlType { get; set; }
    public long GroupId { get; set; }
    public long SubjectId { get; set; }
    public int SemesterNumber { get; set; }
    public int ClassroomWorkHours { get; set; }
    public int IndependentWorkHours { get; set; }
}