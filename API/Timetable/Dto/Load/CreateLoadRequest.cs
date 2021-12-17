using Model.Entities;

namespace API.Timetable.Dto.Load;

public class CreateLoadRequest
{
    public long DisciplineId { get; set; }
    public long TeacherId { get; set; }
    public TeacherLoad.ClassType Type { get; set; }
    public int TotalHours { get; set; }
}