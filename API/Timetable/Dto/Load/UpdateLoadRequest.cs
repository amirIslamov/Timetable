using Model.Entities;

namespace API.Timetable.Dto.Load;

public class UpdateLoadRequest
{
    public TeacherLoad.ClassType Type { get; set; }
    public long TeacherId { get; set; }
    public int TotalHours { get; set; }
}