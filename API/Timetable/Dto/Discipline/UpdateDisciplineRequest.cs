namespace API.Timetable.Dto.Discipline;

public class UpdateDisciplineRequest
{
    public Model.Entities.Discipline.SemesterControlType ControlType { get; set; }
    public long SubjectId { get; set; }
    public int SemesterNumber { get; set; }
    public int ClassroomWorkHours { get; set; }
    public int IndependentWorkHours { get; set; }
}