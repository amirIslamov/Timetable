namespace Model.Entities;

public class Discipline
{
    public enum SemesterControlType
    {
        Offset,
        Exam
    }

    public long Id { get; set; }

    public int SemesterNumber { get; set; }
    public int ClassroomWorkHours { get; set; }
    public int IndependentWorkHours { get; set; }
    public SemesterControlType ControlType { get; set; }

    public long SubjectId { get; set; }
    public Subject Subject { get; set; }

    public long GroupId { get; set; }
    public Group Group { get; set; }
}